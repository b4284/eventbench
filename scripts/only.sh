#!/usr/bin/env bash
# Keep only the given containers running: start them (in order) and stop every other container.
# This session is the sole podman user, so anything not needed by the next run is stopped to free CPU/RAM.
# usage: scripts/only.sh eb-postgres eb-pgbouncer eb-redis ...
#   aliases: pg = eb-postgres + all eb-pgbouncer*, redis, kafka, nats, rabbitmq, mssql, mongo,
#            redis-ha = 3 redis + 3 sentinels, temporal (start it fresh with infra/temporal/up.sh),
#            plus any literal container name
set -uo pipefail
want=()
for a in "$@"; do
  case "$a" in
    pg) want+=(eb-postgres $(podman ps -a --format '{{.Names}}' | grep -E '^eb-pgbouncer(-[0-9]+)?$')) ;;
    pg-ha) want+=(eb-postgres $(podman ps -a --format '{{.Names}}' | grep -E '^eb-pgbouncer(-[0-9]+)?$') eb-pg-standby) ;;
    nats-cluster) want+=(eb-nats-1 eb-nats-2 eb-nats-3) ;;
    kafka-cluster) want+=(eb-kafka-1 eb-kafka-2 eb-kafka-3) ;;
    redis-ha) want+=(eb-redis-6380 eb-redis-6381 eb-redis-6382 eb-sentinel-26380 eb-sentinel-26381 eb-sentinel-26382) ;;
    redis|kafka|nats|rabbitmq|mssql|mongo) want+=("eb-$a") ;;
    temporal) want+=(eb-temporal) ;;
    *) want+=("$a") ;;
  esac
done
is_wanted() { local n; for n in "${want[@]}"; do [[ "$n" == "$1" ]] && return 0; done; return 1; }
for c in $(podman ps --format '{{.Names}}'); do
  [[ "$c" == *-infra ]] && continue          # pod infra containers follow their pod
  is_wanted "$c" || { podman stop -t 20 "$c" >/dev/null 2>&1 && echo "stopped $c"; }
done
for c in "${want[@]}"; do
  [[ "$(podman inspect -f '{{.State.Running}}' "$c" 2>/dev/null)" == true ]] || { podman start "$c" >/dev/null && echo "started $c"; }
done
# Wait until freshly started services are actually ready.
ready() {
  case "$1" in
    eb-postgres) podman exec eb-postgres pg_isready -U bench -d bench -h 127.0.0.1 >/dev/null 2>&1 ;;
    eb-kafka-[123]) podman exec "$1" /opt/kafka/bin/kafka-topics.sh --bootstrap-server 127.0.0.1:9${1##*-}92 --list >/dev/null 2>&1 ;;
    eb-kafka)    podman exec eb-kafka /opt/kafka/bin/kafka-topics.sh --bootstrap-server 127.0.0.1:9092 --list >/dev/null 2>&1 \
                   || podman exec eb-kafka kafka-topics --bootstrap-server 127.0.0.1:9092 --list >/dev/null 2>&1 ;;
    eb-nats)     curl -fsS "http://127.0.0.1:8222/healthz?js-enabled-only=true" >/dev/null 2>&1 ;;
    eb-rabbitmq) podman exec --user rabbitmq eb-rabbitmq rabbitmq-diagnostics -q check_port_connectivity >/dev/null 2>&1 ;;
    eb-redis)    [[ "$(podman exec eb-redis redis-cli ping 2>/dev/null)" == PONG ]] ;;
    *) true ;;
  esac
}
for c in "${want[@]}"; do
  for _ in $(seq 1 90); do ready "$c" && break; sleep 1; done
done
# Kafka's group coordinator needs a few more seconds after the listener answers.
for c in "${want[@]}"; do [[ "$c" == eb-kafka || "$c" == eb-kafka-1 ]] && sleep 10; done
sleep "${ONLY_SETTLE:-2}"
