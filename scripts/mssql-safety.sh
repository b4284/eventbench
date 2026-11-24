#!/usr/bin/env bash
# matrix-38: SQL Server In-Memory OLTP vs Postgres: durable commits, crash drills, the full stack (B16, B33).
# usage: scripts/mssql-safety.sh <phase> [names...]     phases: perf | crash | full | fullcrash | agperf | agcrash
# Every run: 500 groups, 16k sources, ~16k ev/s offered (RateMultiplier 1.24), 3 dispatchers, ZeroMQ, 60 s.
set -uo pipefail
cd "$(dirname "$0")/.."
O=${MATRIX_DIR:-results/matrix-38-mssql-safety}
mkdir -p "$O"
export EB_MQTT=zmq
PW='EventBench#2026'
ZMQ=(--Broker:Transport=zmq --Broker:ZmqPeers=d1@127.0.0.1:7101,d2@127.0.0.1:7103,d3@127.0.0.1:7105)
BASE=(-c configs/pgmq-500groups.json -c configs/overlay-slow.json -c configs/overlay-octopus-worker.json
      --Workload:RateMultiplier=1.24 --Store:BatchCommits=true --Store:BatchExecutors=16 --Store:BatchMaxSize=32 "${ZMQ[@]}")
MSADMIN=(--Broker:AdminDatabase=sqlserver "--Broker:AdminConnectionString=Server=127.0.0.1,1433;Database=octopus;User Id=sa;Password=$PW;TrustServerCertificate=true;Connect Timeout=5")
# the application's writes on SQL Server (database "bench"; its durability set per run)
MSSTORE=(--Store:Type=sqlserver "--Store:ConnectionString=Server=127.0.0.1,1433;Database=bench;User Id=sa;Password=$PW;TrustServerCertificate=true;Max Pool Size=400;Connect Timeout=5" "--Store:AdminConnectionString=Server=127.0.0.1,1433;Database=bench;User Id=sa;Password=$PW;TrustServerCertificate=true;Connect Timeout=5")

row() {
  local n=$1 d; d=$(command ls -d "$O/$n"/*/ 2>/dev/null | tail -1); [[ -z $d ]] && { echo "$n: no result"; return; }
  local s=$d/summary.md t e g
  t=$(grep -A4 '^## Throughput' "$s" | tail -1); e=$(grep -E "^\| e2e \|" "$s")
  g() { grep -m1 "^| $1 |" "$s" | cut -d'|' -f3 | tr -d ' '; }
  local fails; fails=$(awk '/^## Oracle/{f=1} f && /\| fail \|/{split($0,a,"|"); printf "%s ", a[2]}' "$s")
  local ag=""; grep -q "^| ctr_eb_ag1_cpu_pct |" "$s" && ag=" | ag1 $(g ctr_eb_ag1_cpu_pct)% ag2 $(g ctr_eb_ag2_cpu_pct)%"
  grep -q "^| ctr_eb_pg_standby_cpu_pct |" "$s" && ag=" | standby $(g ctr_eb_pg_standby_cpu_pct)%"
  printf '%s | prod/s %s | appl/s %s | p50 %s ms | p99 %s ms | mssql %s%% | pg %s%% | disp %s | host %s%%%s | fails: %s\n' "$n" \
    "$(cut -d'|' -f4 <<<"$t" | tr -d ' ')" "$(cut -d'|' -f5 <<<"$t" | tr -d ' ')" "$(cut -d'|' -f5 <<<"$e" | tr -d ' ')" \
    "$(cut -d'|' -f7 <<<"$e" | tr -d ' ')" "$(g ctr_eb_mssql_cpu_pct)" "$(g ctr_eb_postgres_cpu_pct)" \
    "$(g octopus_dispatcher_cores)" "$(g host_cpu_pct)" "$ag" "${fails:-none}"
}
mssql_db() { podman exec eb-mssql /opt/mssql-tools18/bin/sqlcmd -C -S 127.0.0.1 -U sa -P "$PW" -b -h -1 -Q "SET NOCOUNT ON; ALTER DATABASE bench SET DELAYED_DURABILITY = $1 WITH NO_WAIT" >/dev/null; }
procstats() {
  podman exec "$MSSQL_CTR" /opt/mssql-tools18/bin/sqlcmd -C -S "$MSSQL_SRV" -U sa -P "$PW" -d octopus -W -s '|' -Q "SET NOCOUNT ON; SELECT TOP 6 OBJECT_NAME(object_id, database_id) AS p, execution_count AS calls, CAST(total_worker_time/1e6 AS decimal(9,1)) AS cpu_s, CAST(total_elapsed_time/1e3/NULLIF(execution_count,0) AS decimal(9,2)) AS ms_per_call FROM sys.dm_exec_procedure_stats WHERE database_id = DB_ID('octopus') ORDER BY total_worker_time DESC" 2>/dev/null | grep -v "^-" > "$O/$1.procs.log"
  podman exec "$MSSQL_CTR" /opt/mssql-tools18/bin/sqlcmd -C -S "$MSSQL_SRV" -U sa -P "$PW" -W -s '|' -Q "SET NOCOUNT ON; SELECT TOP 10 wait_type, waiting_tasks_count, wait_time_ms FROM sys.dm_os_wait_stats WHERE wait_type NOT LIKE '%SLEEP%' AND wait_type NOT LIKE 'XE%' AND wait_type NOT IN ('BROKER_TASK_STOP','DIRTY_PAGE_POLL','HADR_FILESTREAM_IOMGR_IOCOMPLETION','SP_SERVER_DIAGNOSTICS_SLEEP','REQUEST_FOR_DEADLOCK_SEARCH','LOGMGR_QUEUE','CHECKPOINT_QUEUE','XE_DISPATCHER_WAIT','FT_IFTS_SCHEDULER_IDLE_WAIT','SQLTRACE_INCREMENTAL_FLUSH_SLEEP','WAIT_XTP_OFFLINE_CKPT_NEW_LOG','PWAIT_EXTENSIBILITY_CLEANUP_TASK','QDS_PERSIST_TASK_MAIN_LOOP_SLEEP','QDS_ASYNC_QUEUE','WAIT_XTP_HOST_WAIT','PREEMPTIVE_OS_FLUSHFILEBUFFERS') ORDER BY wait_time_ms DESC" 2>/dev/null | grep -v "^-" >> "$O/$1.procs.log"
}
clearwaits() { podman exec "$MSSQL_CTR" /opt/mssql-tools18/bin/sqlcmd -C -S "$MSSQL_SRV" -U sa -P "$PW" -Q "DBCC SQLPERF('sys.dm_os_wait_stats', CLEAR)" >/dev/null 2>&1; }

AGADMIN=(--Broker:AdminDatabase=sqlserver "--Broker:AdminConnectionString=Server=127.0.0.1,14330;Database=octopus;User Id=sa;Password=$PW;TrustServerCertificate=true;Connect Timeout=5")
AGSTORE=(--Store:Type=sqlserver "--Store:ConnectionString=Server=127.0.0.1,14330;Database=bench;User Id=sa;Password=$PW;TrustServerCertificate=true;Max Pool Size=400;Connect Timeout=5" "--Store:AdminConnectionString=Server=127.0.0.1,14330;Database=bench;User Id=sa;Password=$PW;TrustServerCertificate=true;Connect Timeout=5")
MSSQL_CTR=eb-mssql MSSQL_SRV=127.0.0.1

# run <name> <mssql|pg> <forced|durable> <drill action> <store: mem|db>
run() {
  local n=$1 be=$2 dur=$3 act=$4 st=$5 args=("${BASE[@]}")
  pgrep -f "/target/release/octopusd$" | xargs -r kill; sleep 2
  unset MSSQL_AG
  if [[ $be == ag ]]; then
    export OCTOPUS_BACKEND=mssql OCTOPUSD_ENV="OCTOPUS_MSSQL_INMEMORY=1 ${OCTOPUSD_EXTRA:-}" MSSQL_AG=1 MSSQL_DELAYED_DURABILITY=DISABLED
    args+=("${AGADMIN[@]}")
  elif [[ $be == mssql ]]; then
    export OCTOPUS_BACKEND=mssql OCTOPUSD_ENV="OCTOPUS_MSSQL_INMEMORY=1 ${OCTOPUSD_EXTRA:-}"
    export MSSQL_DELAYED_DURABILITY=$([[ $dur == durable ]] && echo DISABLED || echo FORCED)
    args+=("${MSADMIN[@]}")
    mssql_db "$MSSQL_DELAYED_DURABILITY"
  else
    unset OCTOPUS_BACKEND OCTOPUSD_ENV MSSQL_DELAYED_DURABILITY
    infra/pg-sync-commit.sh "$([[ $dur == durable ]] && echo on || echo off)" >/dev/null
  fi
  if [[ $st == mem ]]; then args+=(--Store:Type=inmemory)
  elif [[ $be == mssql ]]; then args+=("${MSSTORE[@]}")
  elif [[ $be == ag ]]; then args+=("${AGSTORE[@]}")
  fi   # pg + db: the overlay's postgres store
  [[ $be == ag ]] && MSSQL_CTR=eb-$(cd infra/mssql-ag && source ./env.sh && primary) MSSQL_SRV=127.0.0.1,14330 || MSSQL_CTR=eb-mssql MSSQL_SRV=127.0.0.1
  [[ -n ${EXTRA:-} ]] && read -r -a extra <<<"$EXTRA" && args+=("${extra[@]}")
  clearwaits
  DRILL_TIMEOUT=${DRILL_TIMEOUT:-900} scripts/octopus-drill.sh "$O" "$n" 3 "$act" "${args[@]}" > /dev/null 2>&1
  [[ $be == mssql || $be == ag ]] && { MSSQL_CTR=eb-$( [[ $be == ag ]] && (cd infra/mssql-ag && source ./env.sh && primary) || echo mssql); procstats "$n"; }
  [[ $be == ag ]] && infra/mssql-ag/status.sh > "$O/$n.ag.log" 2>&1
  row "$n" | tee -a "$O/rows.log"
  [[ $act != none ]] && sed 's/^/    /' "$O/$n.drill.log" | tee -a "$O/rows.log"
}

phase=${1:?perf|crash|full|fullcrash}; shift
case $phase in
  perf)  # Octopus alone (in-memory store): best effort vs durable, both engines, interleaved
    for r in a b; do
      run F$r-mssql mssql forced none mem; run D$r-mssql mssql durable none mem
      run P$r-pg pg forced none mem;       run Q$r-pg pg durable none mem
    done ;;
  crash) # kill -9 of the database 30 s into the run, restarted 5 s later; the oracle checks every event
    run Ka-mssql-durable mssql durable mssql-kill:5 mem
    run Kb-pg-durable pg durable pg-kill:5 mem
    run Kc-mssql-forced mssql forced mssql-kill:5 mem
    run Kd-pg-forced pg forced pg-kill:5 mem
    run Ke-mssql-durable mssql durable mssql-kill:5 mem ;;
  full)  # Octopus and the application's writes on the same engine, durable
    for r in a b; do run S$r-mssql mssql durable none db; run T$r-pg pg durable none db; done ;;
  fullms) for r in a b; do run S$r-mssql mssql durable none db; done ;;
  fullbatch) # the SQL Server store with group commit (as the Postgres store's), and Postgres without it
    for r in c d; do run S$r-mssql-batch mssql durable none db; done
    EXTRA="--Store:BatchCommits=false" run Ua-pg-nobatch pg durable none db ;;
  agperf) # the availability group (infra/mssql-ag/up.sh first): synchronous commit, durable; alone and full stack
    for r in a b; do run A$r-ag-mem ag durable none mem; run B$r-ag-full ag durable none db; done ;;
  pgsync) # Postgres with a synchronous standby (infra/pg-standby/up.sh, synchronous_standby_names = eb_standby):
          # synchronous_commit = on waits for the standby's flush, as an AG's synchronous commit hardens the log
    for r in a b; do run R$r-pgsync-mem pg durable none mem; run W$r-pgsync-full pg durable none db; done ;;
  agtune) # more commits in flight against the AG's synchronous commit latency
    EXTRA="--Store:BatchExecutors=64 --Store:BatchMaxSize=64" run Bc-ag-full-x64 ag durable none db
    EXTRA="--Store:BatchExecutors=64 --Store:BatchMaxSize=64" OCTOPUS_PG_POOL=64 run Bd-ag-full-x64-pool64 ag durable none db ;;
  agslots) # worker concurrency sized for the synchronous commit's latency: 4 events per worker in flight
    EXTRA="--Worker:MaxParallelPerWorker=4" run Be-ag-full-par4 ag durable none db
    EXTRA="--Worker:MaxParallelPerWorker=4" run Bf-ag-full-par4 ag durable none db ;;
  bodies) # matrix-39 (B37): publish's bodies as base64 in the JSON vs one raw varbinary parameter; durable, single node
    for r in a b; do
      OCTOPUSD_EXTRA=OCTOPUS_MSSQL_BODIES=json run J$r-json mssql durable none mem
      OCTOPUSD_EXTRA=OCTOPUS_MSSQL_BODIES=blob run J$r-blob mssql durable none mem
    done
    OCTOPUSD_EXTRA=OCTOPUS_MSSQL_BODIES=blob run Sa-full-blob mssql durable none db ;;
  storetvp) # matrix-39 (B37): the store's group commit as N EXECs in one transaction vs one set-based TVP call
    for r in a b; do
      EXTRA="--Store:BatchMode=exec" run X$r-full-exec mssql durable none db
      EXTRA="--Store:BatchMode=tvp" run X$r-full-tvp mssql durable none db
    done ;;
  gaps) # B38: are SQL Server's gap reports concurrent publishes of one source? (one in flight per source: expect none)
    EXTRA="--Workload:SourcePipeline=1" run G1-pipe1 mssql durable none mem
    run G4-pipe4 mssql durable none mem ;;
  gapsfix) # B38 after the fix (the hint's cursor re-read in the same statement as the event table)
    EXTRA="--Workload:SourcePipeline=1" run H1-pipe1-fix mssql durable none mem
    run H4-pipe4-fix mssql durable none mem ;;
  ramp) # matrix-40: latency while the laptop keeps up; no deliberately slow events; durable (B, local part)
    for m in 0.31:4k 0.62:8k 0.93:12k 1.24:16k 1.55:20k; do
      x=${m%%:*}; l=${m##*:}; E="--Workload:SlowEvents:Fraction=0 --Workload:RateMultiplier=$x"
      EXTRA="$E" run pg-mem-$l pg durable none mem
      EXTRA="$E" run ms-mem-$l mssql durable none mem
      [[ $l == 20k ]] && continue
      EXTRA="$E" run pg-full-$l pg durable none db
      EXTRA="$E" run ms-full-$l mssql durable none db
    done ;;
  agcrash) # kill -9 of the AG primary, forced failover to the secondary 2 s later, old primary back 30 s later
    run Ca-ag-mem ag durable ag-kill:30 mem; run Cb-ag-full ag durable ag-kill:30 db ;;
  fullcrash)
    run Xa-mssql mssql durable mssql-kill:5 db; run Xb-pg pg durable pg-kill:5 db ;;
esac
infra/pg-sync-commit.sh off >/dev/null; podman container exists eb-mssql && [[ $(podman inspect -f "{{.State.Running}}" eb-mssql) == true ]] && mssql_db FORCED
echo "$phase done"
