#!/usr/bin/env bash
# One Octopus performance run on the single eb-postgres (no standby: B26), with pg_stat_statements sampled from
# 30 s to 70 s into the run and the dispatchers' last stats line.
# usage: [NDISP=3] scripts/perf-pgss.sh <out-dir> <name> <octopus-repo> [benchmark args...]
set -uo pipefail
O=$1; N=$2; R=$3; shift 3
mkdir -p "$O"
export EB_MQTT=${EB_MQTT:-mosquitto}
if podman ps --format '{{.Names}}' | grep -qE 'standby|patroni|geo-|eb-b-'; then echo "a standby/cluster is running: stop it first (B26)" >&2; exit 2; fi
pgrep -f "/target/release/octopusd$" | xargs -r kill; sleep 2
( sleep 30; podman exec eb-postgres psql -U bench -d bench -qc "select pg_stat_statements_reset()" >/dev/null; sleep 40
  podman exec eb-postgres psql -U bench -d bench -c "select calls, round(total_exec_time::numeric/1000,1) as total_s, round(mean_exec_time::numeric,3) as mean_ms, left(regexp_replace(query, '\s+', ' ', 'g'), 70) q from pg_stat_statements order by total_exec_time desc limit 8" > "$O/$N.pgss.log" 2>&1
  podman exec eb-postgres psql -U bench -d bench -Atc "select 'event table at 70 s: ' || pg_size_pretty(pg_total_relation_size('octopus.event')) || ', live ' || n_live_tup || ', dead ' || n_dead_tup || ', autovacuums ' || autovacuum_count from pg_stat_user_tables where relid = 'octopus.event'::regclass" >> "$O/$N.pgss.log" 2>&1 ) &
# wait events of active backends, sampled every 100 ms from 35 s to 55 s into the run
( sleep 35; podman exec eb-postgres psql -U bench -d bench -Atq -c "create temp table w (k text, q text); do \$\$ begin for i in 1..200 loop perform pg_stat_clear_snapshot(); insert into w select coalesce(wait_event_type || ':' || wait_event, 'CPU'), left(regexp_replace(query, '\s+', ' ', 'g'), 40) from pg_stat_activity where state = 'active' and pid <> pg_backend_pid() and backend_type = 'client backend'; perform pg_sleep(0.1); end loop; end \$\$;" \
    -c "select count(*) || ' | ' || k || ' | ' || q from w group by k, q order by count(*) desc limit 14" > "$O/$N.waits.log" 2>&1 ) &
OCTOPUS_REPO=$R scripts/octopus-drill.sh "$O" "$N" "${NDISP:-3}" none -c configs/pgmq-500groups.json -c configs/overlay-slow.json \
  -c configs/overlay-octopus-worker.json --Workload:RateMultiplier=1.24 --Store:BatchCommits=true --Store:BatchExecutors=16 \
  --Store:BatchMaxSize=32 "$@" > /dev/null 2>&1
wait
d=$(command ls -d "$O/$N"/*/ | tail -1)
t=$(grep -A4 '^## Throughput' "$d/summary.md" | tail -1)
e=$(grep -E "^\| e2e \|" "$d/summary.md")
pg=$(grep -m1 -iE "postgres.*cores|eb-postgres" "$d/summary.md" | cut -c1-80)
echo "$N: produced/s $(cut -d'|' -f4 <<<"$t") applied/s $(cut -d'|' -f5 <<<"$t") blocking $(cut -d'|' -f12 <<<"$t") e2e p50 $(cut -d'|' -f5 <<<"$e") p99 $(cut -d'|' -f7 <<<"$e") | $pg"
scripts/octopus-stats.py "$O/$N".d?.log
head -11 "$O/$N.pgss.log" | tail -9; grep "event table" "$O/$N.pgss.log"
echo "-- wait samples (count | wait | statement):"; cat "$O/$N.waits.log"
