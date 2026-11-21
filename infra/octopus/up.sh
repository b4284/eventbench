#!/usr/bin/env bash
# Fresh Octopus cluster for one benchmark run:
#   - Mosquitto (MQTT 5) container eb-mosquitto on 127.0.0.1:1883 (started if needed; or another broker, or NATS: EB_MQTT)
#   - the octopus schema dropped (every run starts empty) and the bench hooks installed
#   - N octopusd processes (members d1..dN) built from the Octopus repository next to this one,
#     connected directly to Postgres (5432); logs and pids under ~/.cache/eventbench/octopus
# usage: infra/octopus/up.sh [N=3]      extra env for the dispatchers: OCTOPUSD_ENV="OCTOPUS_X=y ..."
#        OCTOPUSD_PREFIX: command prefix for the dispatchers, e.g. "taskset -c 12-23"
#        OCTOPUS_PG_CONN: the dispatchers' database (default: the primary on 5432); for failover drills a multi-host
#          string such as "host=127.0.0.1,127.0.0.1 port=5432,5433 ... target_session_attrs=read-write"
set -euo pipefail
cd "$(dirname "$0")"
N=${1:-3}
OCTO=${OCTOPUS_REPO:-$(cd ../../.. && pwd)/octopus}
STATE=~/.cache/eventbench/octopus
mkdir -p "$STATE"
./down.sh >/dev/null 2>&1 || true

# MQTT broker: EB_MQTT=mosquitto (single-threaded), nanomq (multi-threaded, MQTT 5) or emqx (multi-core, MQTT 5;
# from 5.9 free for a single node only, clustering needs a license); or EB_MQTT=nats: NATS in place of MQTT (port
# 4222; the dispatchers get OCTOPUS_TRANSPORT=nats, the benchmark needs --Broker:Transport=nats)
MQ=${EB_MQTT:-nanomq}
CTR=eb-$MQ; [[ $MQ == nats ]] && CTR=eb-nats-octopus   # (eb-nats is the JetStream lab's, infra/nats)
for other in eb-mosquitto eb-nanomq eb-emqx eb-nats-octopus; do [[ $other == "$CTR" ]] || podman stop -t 5 $other >/dev/null 2>&1 || true; done
# a running Mosquitto listening on another address than requested is recreated (remote clients need 0.0.0.0)
if [[ $MQ == mosquitto ]] && ! grep -qx "listener 1883 ${EB_MQTT_BIND:-127.0.0.1}" "$STATE/mosquitto.conf" 2>/dev/null; then
  podman rm -f eb-mosquitto >/dev/null 2>&1 || true
fi
if [[ $MQ == emqx ]] && [[ "$(cat "$STATE/emqx.bind" 2>/dev/null)" != "${EB_MQTT_BIND:-127.0.0.1}" ]]; then
  podman rm -f eb-emqx >/dev/null 2>&1 || true
fi
if [[ $MQ == nanomq ]] && ! grep -q "bind = \"${EB_MQTT_BIND:-127.0.0.1}:1883\"" "$STATE/nanomq.conf" 2>/dev/null; then
  podman rm -f eb-nanomq >/dev/null 2>&1 || true
fi
if [[ $MQ == nats ]] && ! grep -q "listen: \"${EB_MQTT_BIND:-127.0.0.1}:4222\"" "$STATE/nats.conf" 2>/dev/null; then
  podman rm -f eb-nats-octopus >/dev/null 2>&1 || true
fi
if [[ "$(podman inspect -f '{{.State.Running}}' "$CTR" 2>/dev/null)" != true ]]; then
  podman rm -f "$CTR" >/dev/null 2>&1 || true
  if [[ $MQ == nats ]]; then
    # events up to tens of MB travel inline (64 MB is NATS's hard limit); slow consumers get time instead of a cut
    cat > "$STATE/nats.conf" <<CONF
listen: "${EB_MQTT_BIND:-127.0.0.1}:4222"
http: "127.0.0.1:8222"
max_payload: 64MB
max_pending: 1GB
write_deadline: "30s"
CONF
    podman run -d --name eb-nats-octopus --network host -v "$STATE/nats.conf:/nats.conf:ro,Z" \
      docker.io/library/nats:${NATS_VERSION:-2.15.2} -c /nats.conf >/dev/null
  elif [[ $MQ == emqx ]]; then
    # Octopus has few, busy connections: big packets (events up to MBs), deep queues, no forced shutdown of busy
    # clients (EMQX's IoT-oriented defaults would kick them), anonymous access as with Mosquitto.
    echo "${EB_MQTT_BIND:-127.0.0.1}" > "$STATE/emqx.bind"
    podman run -d --name eb-emqx --network host \
      -e EMQX_LISTENERS__TCP__DEFAULT__BIND="${EB_MQTT_BIND:-127.0.0.1}:1883" \
      -e EMQX_LISTENERS__SSL__DEFAULT__ENABLE=false -e EMQX_LISTENERS__WS__DEFAULT__ENABLE=false \
      -e EMQX_LISTENERS__WSS__DEFAULT__ENABLE=false -e EMQX_DASHBOARD__LISTENERS__HTTP__BIND=127.0.0.1:18083 \
      -e EMQX_MQTT__MAX_PACKET_SIZE=64MB -e EMQX_MQTT__MAX_MQUEUE_LEN=1000000 -e EMQX_MQTT__MQUEUE_STORE_QOS0=true \
      -e EMQX_FORCE_SHUTDOWN__ENABLE=false -e EMQX_FORCE_GC__ENABLE=false \
      -e EMQX_LISTENERS__TCP__DEFAULT__MAX_CONNECTIONS=100000 \
      docker.io/emqx/emqx:${EMQX_VERSION:-6.3.2} >/dev/null
    for _ in $(seq 1 60); do (echo > /dev/tcp/127.0.0.1/1883) 2>/dev/null && break; sleep 1; done
  elif [[ $MQ == mosquitto ]]; then
    # EB_MQTT_BIND: listen address (default loopback; 0.0.0.0 when clients run on another machine)
    cat > "$STATE/mosquitto.conf" <<CONF
listener 1883 ${EB_MQTT_BIND:-127.0.0.1}
allow_anonymous true
persistence false
max_inflight_messages 0
max_queued_messages 1000000
max_packet_size 67108864
set_tcp_nodelay true
CONF
    podman run -d --name eb-mosquitto --network host -v "$STATE/mosquitto.conf:/mosquitto/config/mosquitto.conf:ro,Z" \
      docker.io/library/eclipse-mosquitto:2 >/dev/null
  else
    cat > "$STATE/nanomq.conf" <<'CONF'
system {
    num_taskq_thread = ${NANOMQ_THREADS}
    max_taskq_thread = ${NANOMQ_THREADS}
    parallel = 64
}
mqtt {
    max_packet_size = 64MB
    max_mqueue_len = 65535
    max_inflight_window = 65535
    retry_interval = 10s
    keepalive_multiplier = 1.5
}
listeners.tcp { bind = "${NANOMQ_BIND}:1883" }
log { to = [console], level = warn }
auth { allow_anonymous = true, no_match = allow, deny_action = ignore }
CONF
    sed -i "s/\${NANOMQ_THREADS}/${NANOMQ_THREADS:-8}/; s/\${NANOMQ_BIND}/${EB_MQTT_BIND:-127.0.0.1}/" "$STATE/nanomq.conf"
    podman run -d --name eb-nanomq --network host -v "$STATE/nanomq.conf:/etc/nanomq.conf:ro,Z" \
      docker.io/emqx/nanomq:0.25.7 >/dev/null
  fi
  sleep 1
fi

# EB_PSQL: how to reach the database for setup (default: the lab primary container; for the Patroni pair, a psql
# with a multi-host connection string that finds the leader)
PSQL=${EB_PSQL:-"podman exec -i eb-postgres psql -U bench -d bench -v ON_ERROR_STOP=1 -q"}
# OCTOPUS_BACKEND=mssql: Octopus on SQL Server (infra/mssql), in a database "octopus" recreated for every run, with
# the lab's best-effort durability (delayed durable commits, like synchronous_commit=off).
BACKEND=${OCTOPUS_BACKEND:-pg}
MSSQL_PASSWORD=${MSSQL_SA_PASSWORD:-EventBench#2026}
mssql() { podman exec -i "${MSSQL_CONTAINER:-eb-mssql}" /opt/mssql-tools18/bin/sqlcmd -C -S 127.0.0.1 -U sa -P "$MSSQL_PASSWORD" -I -b "$@"; }
mysqlc() { podman exec -i "${MYSQL_CONTAINER:-eb-mysql}" mysql -uroot -p"${MYSQL_ROOT_PASSWORD:-bench}" -N -B "$@" 2>/dev/null; }
if [[ $BACKEND == mysql ]]; then
  # statements a killed dispatcher left running would hold the metadata lock and stall the drop
  for id in $(mysqlc -e "SELECT id FROM information_schema.processlist WHERE db = 'octopus' AND id <> CONNECTION_ID()"); do
    mysqlc -e "KILL $id" || true; done
  mysqlc -e "DROP DATABASE IF EXISTS octopus; CREATE DATABASE octopus"
elif [[ $BACKEND == mssql ]]; then
  mssql -Q "IF DB_ID('octopus') IS NOT NULL BEGIN ALTER DATABASE octopus SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE octopus; END;
            CREATE DATABASE octopus
              ON (NAME = octopus_data, FILENAME = '/var/opt/mssql/data/octopus.mdf', SIZE = 2GB, FILEGROWTH = 1GB)
              LOG ON (NAME = octopus_log, FILENAME = '/var/opt/mssql/data/octopus.ldf', SIZE = 4GB, FILEGROWTH = 1GB);
            ALTER DATABASE octopus SET RECOVERY SIMPLE;
            ALTER DATABASE octopus SET READ_COMMITTED_SNAPSHOT ON WITH ROLLBACK IMMEDIATE;
            ALTER DATABASE octopus SET DELAYED_DURABILITY = ${MSSQL_DELAYED_DURABILITY:-FORCED};" >/dev/null
else
  $PSQL -c "DROP SCHEMA IF EXISTS octopus CASCADE"
  $PSQL < hooks.sql
fi

(cd "$OCTO" && cargo build --release -q -p octopusd)
BIN="$OCTO/target/release/octopusd"
# Workers commit their own writes by default; EB_OCTOPUS_HOOKS=1 lets the dispatchers commit through the hooks.
HOOK_ENV=""
[[ "${EB_OCTOPUS_HOOKS:-0}" == 1 ]] && HOOK_ENV="OCTOPUS_APPLY_HOOK=public.eb_octopus_apply OCTOPUS_LOAD_HOOK=public.eb_octopus_load"
# the database for dispatcher i: OCTOPUS_PG_CONN_<i> (e.g. through a proxy a drill can freeze), else OCTOPUS_PG_CONN
conn_for() {
  local v="OCTOPUS_PG_CONN_$1"
  echo "${!v:-${OCTOPUS_PG_CONN:-host=127.0.0.1 port=5432 user=bench password=bench dbname=bench}}"
}
: > "$STATE/pids"
for i in $(seq 1 "$N"); do
  env OCTOPUS_MEMBER_ID="d$i" OCTOPUS_NS=octopus OCTOPUS_SCHEMA=octopus OCTOPUS_BACKEND="$BACKEND" \
      OCTOPUS_PG="$(conn_for "$i")" OCTOPUS_PG_POOL="${OCTOPUS_PG_POOL:-32}" \
      OCTOPUS_MYSQL="${OCTOPUS_MYSQL_CONN:-mysql://root:${MYSQL_ROOT_PASSWORD:-bench}@127.0.0.1:3306/octopus}" \
      OCTOPUS_MSSQL="${OCTOPUS_MSSQL_CONN:-server=tcp:127.0.0.1,1433;database=octopus;user=sa;password=$MSSQL_PASSWORD;TrustServerCertificate=true}" \
      OCTOPUS_TRANSPORT="$([[ $MQ == nats ]] && echo nats || echo mqtt)" \
      ${HOOK_ENV} RUST_LOG="${RUST_LOG:-info}" ${OCTOPUSD_ENV:-} \
      nohup ${OCTOPUSD_PREFIX:-} "$BIN" >"$STATE/d$i.log" 2>&1 &
  echo $! >> "$STATE/pids"
  [[ $i -eq 1 ]] && sleep 1   # the first one creates the schema
done
for _ in $(seq 1 60); do
  if [[ $BACKEND == mysql ]]; then
    n=$(mysqlc -D octopus -e "SELECT COUNT(*) FROM octopus_member WHERE heartbeat > UTC_TIMESTAMP(6) - INTERVAL 5 SECOND" | head -1)
    n=${n:-0}
  elif [[ $BACKEND == mssql ]]; then
    n=$(mssql -d octopus -h -1 -W -Q "SET NOCOUNT ON; IF OBJECT_ID('octopus.member') IS NULL SELECT 0 ELSE SELECT COUNT(*) FROM octopus.member WHERE heartbeat > DATEADD(SECOND, -5, SYSUTCDATETIME())" 2>/dev/null | tr -d ' \r' | head -1)
    n=${n:-0}
  else
    n=$($PSQL -At -c "SELECT count(*) FROM octopus.member WHERE heartbeat > now() - interval '5 seconds'" 2>/dev/null || echo 0)
  fi
  [[ "$n" -ge "$N" ]] && { echo "octopus: $n dispatchers up (pids $(tr '\n' ' ' < "$STATE/pids"))"; exit 0; }
  sleep 0.5
done
echo "dispatchers did not come up" >&2; tail -5 "$STATE"/d*.log >&2; exit 1
