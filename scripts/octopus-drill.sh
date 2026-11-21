#!/usr/bin/env bash
# Octopus failure drill: a fresh cluster of N dispatchers, a benchmark run, and one failure 30 s into the run.
# The run then finishes and drains, and the oracle checks the end state (every event applied once, in order).
#
# usage: scripts/octopus-drill.sh <out-dir> <name> <dispatchers> <action> [benchmark args...]
#   action: none | kill:<i> (SIGKILL dispatcher i) | term:<i> (SIGTERM: planned stop) | add (one more dispatcher)
#           | pgfail (kill the Postgres primary, promote the synchronous standby; needs infra/pg-standby up + sync on)
#           Patroni pair (infra/patroni; use EB_PSQL=infra/patroni/psql.sh and the multi-host OCTOPUS_PG_CONN):
#           | patroni-kill[:<r>] (SIGKILL the leader's container; Patroni promotes the sync standby after the leader
#             lease; the killed node is started again r seconds later (default 30), as a crashed pod comes back. With
#             two nodes in strict synchronous mode, no commit can complete between the failover and its return.)
#           | patroni-freeze:<s> (freeze the leader for s seconds: a hang or partition, not a crash; tests fencing)
#           | db-freeze:<s> (freeze every database node for s seconds: an outage no failover can mend)
#           | partition-dc1:<s> (bridged lab, infra/patroni-bridged: cut DC1 from the WAN for s seconds; it keeps running)
#           | node-kill (SIGKILL the leader, not restarted) | dc-kill:leader|other (SIGKILL a DC: its database nodes and
#             its etcd member) | dc-freeze:leader|other:<s> (freeze a DC for s seconds: a partition, tests fencing)
#           | mqtt-restart (restart the MQTT broker) | dispatcher-db-cut:<i>:<s> (dispatcher i keeps running, but its
#             database connections hang for s seconds: it goes through a proxy that is frozen)
# env: DRILL_AT (seconds into the run, default 30); OCTOPUS_PG_CONN for the dispatchers (multi-host for pgfail)
set -uo pipefail
cd "$(dirname "$0")/.."
OUT=$1; NAME=$2; N=$3; ACTION=$4; shift 4
AT=${DRILL_AT:-30}
BIN=src/EventBench.Cli/bin/Release/net10.0/eventbench.dll
STATE=~/.cache/eventbench/octopus
mkdir -p "$OUT"
if pgrep -f "eventbench.dll run" >/dev/null; then echo "another eventbench run is active; aborting" >&2; exit 3; fi
PROXY=""
if [[ $ACTION == dispatcher-db-cut:* ]]; then
  i=$(cut -d: -f2 <<<"$ACTION")
  scripts/tcp-proxy.py 15441:5441 15442:5442 & PROXY=$!
  export "OCTOPUS_PG_CONN_$i=host=127.0.0.1,127.0.0.1 port=15441,15442 user=bench password=bench dbname=bench target_session_attrs=read-write connect_timeout=2"
  sleep 0.5
fi
infra/octopus/up.sh "$N" > "$OUT/$NAME.pre.log" 2>&1 || { echo "cluster did not start"; exit 2; }
LOG="$OUT/$NAME.log"
# a hard limit, so a drill that blocks the database for good cannot hang for hours (DRILL_TIMEOUT seconds)
timeout --signal=INT --kill-after=30 "${DRILL_TIMEOUT:-900}" dotnet "$BIN" run "$@" --Name="$NAME" --ResultsDirectory="$OUT/$NAME" --Metrics:ConsoleProgress=false > "$LOG" 2>&1 &
BENCH=$!
until grep -q " run: " "$LOG" 2>/dev/null; do kill -0 $BENCH 2>/dev/null || break; sleep 0.5; done
# Patroni helpers: the node registry is ~/.cache/eventbench/patroni/nodes ("name pgport apiport dc")
NODES_FILE=~/.cache/eventbench/patroni/nodes
dc_of() { awk -v n="$1" '$1==n{print $4}' "$NODES_FILE"; }
pick_dc() { local l; l=$(infra/patroni/leader.sh); if [[ $1 == leader ]]; then dc_of "$l"; elif [[ $1 == other ]]; then awk -v d="$(dc_of "$l")" '$4!=d{print $4; exit}' "$NODES_FILE"; else echo "$1"; fi; }
dc_containers() { awk -v d="$1" '$4==d{printf "eb-%s ", $1}' "$NODES_FILE"; podman container exists "eb-etcd-$1" && echo -n "eb-etcd-$1"; }
wait_new_leader() { for _ in $(seq 1 240); do n=$(infra/patroni/leader.sh 2>/dev/null) && [[ -n $n && $n != "$1" ]] && { echo "$(date +%T.%3N) new leader $n"; return; }; sleep 0.5; done; echo "no new leader within 120 s"; }
sleep "$AT"
{
  echo "$(date +%T.%3N) drill: $ACTION"
  case "$ACTION" in
    none) ;;
    kill:*) i=${ACTION#kill:}; pid=$(sed -n "${i}p" "$STATE/pids"); kill -KILL "$pid" && echo "killed d$i (pid $pid)" ;;
    term:*) i=${ACTION#term:}; pid=$(sed -n "${i}p" "$STATE/pids"); kill -TERM "$pid" && echo "stopped d$i (pid $pid)" ;;
    add) infra/octopus/add.sh ;;
    pgfail) infra/pg-standby/failover.sh ;;
    patroni-kill*) r=${ACTION#patroni-kill}; r=${r#:}; r=${r:-30}; l=$(infra/patroni/leader.sh); podman kill eb-$l >/dev/null && echo "killed leader $l"
      t0=$(date +%s); wait_new_leader "$l"
      sleep $(( r - ($(date +%s) - t0) > 0 ? r - ($(date +%s) - t0) : 0 )); podman start eb-$l >/dev/null; echo "$(date +%T.%3N) restarted $l"
      for _ in $(seq 1 240); do infra/patroni/status.sh 2>/dev/null | awk -v n="$l" '$1==n && $2!="leader" && $3=="streaming"{f=1} END{exit !f}' && { echo "$(date +%T.%3N) $l rejoined as standby"; break; }; sleep 0.5; done ;;
    node-kill) l=$(infra/patroni/leader.sh); podman kill eb-$l >/dev/null && echo "killed leader $l (not restarted)"; wait_new_leader "$l" ;;
    patroni-freeze:*) s=${ACTION#patroni-freeze:}; l=$(infra/patroni/leader.sh); podman pause eb-$l >/dev/null && echo "froze leader $l for ${s}s"
      wait_new_leader "$l"; sleep "$s"; podman unpause eb-$l >/dev/null; echo "$(date +%T.%3N) unfroze $l" ;;
    dc-kill:*) dc=$(pick_dc "${ACTION#dc-kill:}"); l=$(infra/patroni/leader.sh); c=$(dc_containers "$dc")
      podman kill $c >/dev/null && echo "killed $dc: $c (leader was $l)"; [[ $(dc_of "$l") == "$dc" ]] && wait_new_leader "$l" ;;
    dc-freeze:*) spec=${ACTION#dc-freeze:}; dc=$(pick_dc "${spec%%:*}"); s=${spec##*:}; l=$(infra/patroni/leader.sh); c=$(dc_containers "$dc")
      podman pause $c >/dev/null && echo "froze $dc for ${s}s: $c (leader was $l)"; [[ $(dc_of "$l") == "$dc" ]] && wait_new_leader "$l"
      sleep "$s"; podman unpause $c >/dev/null; echo "$(date +%T.%3N) unfroze $dc" ;;
    db-freeze:*) s=${ACTION#db-freeze:}; c=$(awk '{printf "eb-%s ", $1}' ~/.cache/eventbench/patroni/nodes); podman pause $c >/dev/null && echo "froze every database node for ${s}s"; sleep "$s"; podman unpause $c >/dev/null; echo "$(date +%T.%3N) unfroze" ;;
    mqtt-restart) podman restart -t 1 eb-mosquitto >/dev/null; echo "$(date +%T.%3N) restarted the MQTT broker" ;;
    partition-dc1:*) s=${ACTION#partition-dc1:}; c="eb-b-dc1a eb-b-dc1b eb-b-etcd-dc1"
      before=$(infra/patroni-bridged/status.sh | awk '$2=="leader"{print $1}')
      for x in $c; do podman network disconnect eb-b-wan $x; done; echo "$(date +%T.%3N) DC1 cut from the WAN for ${s}s (leader was $before)"
      end=$((SECONDS + s)); while (( SECONDS < end )); do l=$(infra/patroni-bridged/status.sh 2>/dev/null | awk '$2=="leader"{print $1}' | tr '\n' ' '); echo "$(date +%T.%3N) leaders: ${l:-none}"; sleep 5; done | awk '!seen[$3 $4]++'
      for x in $c; do podman network connect eb-b-wan $x; done; echo "$(date +%T.%3N) partition healed" ;;
    dispatcher-db-cut:*) s=$(cut -d: -f3 <<<"$ACTION"); kill -STOP $PROXY && echo "cut dispatcher $(cut -d: -f2 <<<"$ACTION") from the database for ${s}s"; sleep "$s"; kill -CONT $PROXY; echo "$(date +%T.%3N) reconnected" ;;
  esac
} > "$OUT/$NAME.drill.log" 2>&1
wait $BENCH
rc=$?
[[ -n $PROXY ]] && kill $PROXY 2>/dev/null
for f in "$STATE"/d*.log; do [[ -f $f ]] && cp "$f" "$OUT/$NAME.$(basename "$f")"; done
echo "$(date +%T) $NAME -> exit $rc"
