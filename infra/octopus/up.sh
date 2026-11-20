#!/usr/bin/env bash
# Fresh Octopus cluster for one benchmark run:
#   - Mosquitto (MQTT 5) container eb-mosquitto on 127.0.0.1:1883 (started if needed)
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

# MQTT broker: EB_MQTT=mosquitto (single-threaded) or nanomq (multi-threaded, MQTT 5)
MQ=${EB_MQTT:-nanomq}
for other in eb-mosquitto eb-nanomq; do [[ $other == eb-$MQ ]] || podman stop -t 5 $other >/dev/null 2>&1 || true; done
if [[ "$(podman inspect -f '{{.State.Running}}' eb-$MQ 2>/dev/null)" != true ]]; then
  podman rm -f eb-$MQ >/dev/null 2>&1 || true
  if [[ $MQ == mosquitto ]]; then
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
listeners.tcp { bind = "127.0.0.1:1883" }
log { to = [console], level = warn }
auth { allow_anonymous = true, no_match = allow, deny_action = ignore }
CONF
    sed -i "s/\${NANOMQ_THREADS}/${NANOMQ_THREADS:-8}/" "$STATE/nanomq.conf"
    podman run -d --name eb-nanomq --network host -v "$STATE/nanomq.conf:/etc/nanomq.conf:ro,Z" \
      docker.io/emqx/nanomq:latest >/dev/null
  fi
  sleep 1
fi

PSQL="podman exec -i eb-postgres psql -U bench -d bench -v ON_ERROR_STOP=1 -q"
$PSQL -c "DROP SCHEMA IF EXISTS octopus CASCADE"
$PSQL < hooks.sql

(cd "$OCTO" && cargo build --release -q -p octopusd)
BIN="$OCTO/target/release/octopusd"
# Workers commit their own writes by default; EB_OCTOPUS_HOOKS=1 lets the dispatchers commit through the hooks.
HOOK_ENV=""
[[ "${EB_OCTOPUS_HOOKS:-0}" == 1 ]] && HOOK_ENV="OCTOPUS_APPLY_HOOK=public.eb_octopus_apply OCTOPUS_LOAD_HOOK=public.eb_octopus_load"
: > "$STATE/pids"
for i in $(seq 1 "$N"); do
  env OCTOPUS_MEMBER_ID="d$i" OCTOPUS_NS=octopus OCTOPUS_SCHEMA=octopus \
      OCTOPUS_PG="${OCTOPUS_PG_CONN:-host=127.0.0.1 port=5432 user=bench password=bench dbname=bench}" OCTOPUS_PG_POOL="${OCTOPUS_PG_POOL:-32}" \
      ${HOOK_ENV} RUST_LOG="${RUST_LOG:-info}" ${OCTOPUSD_ENV:-} \
      nohup ${OCTOPUSD_PREFIX:-} "$BIN" >"$STATE/d$i.log" 2>&1 &
  echo $! >> "$STATE/pids"
  [[ $i -eq 1 ]] && sleep 1   # the first one creates the schema
done
for _ in $(seq 1 60); do
  n=$($PSQL -At -c "SELECT count(*) FROM octopus.member WHERE heartbeat > now() - interval '5 seconds'" 2>/dev/null || echo 0)
  [[ "$n" -ge "$N" ]] && { echo "octopus: $n dispatchers up (pids $(tr '\n' ' ' < "$STATE/pids"))"; exit 0; }
  sleep 0.5
done
echo "dispatchers did not come up" >&2; tail -5 "$STATE"/d*.log >&2; exit 1
