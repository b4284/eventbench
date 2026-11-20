#!/usr/bin/env bash
# Octopus failure drill: a fresh cluster of N dispatchers, a benchmark run, and one failure 30 s into the run.
# The run then finishes and drains, and the oracle checks the end state (every event applied once, in order).
#
# usage: scripts/octopus-drill.sh <out-dir> <name> <dispatchers> <action> [benchmark args...]
#   action: none | kill:<i> (SIGKILL dispatcher i) | term:<i> (SIGTERM: planned stop) | add (one more dispatcher)
#           | pgfail (kill the Postgres primary, promote the synchronous standby; needs infra/pg-standby up + sync on)
#           Patroni pair (infra/patroni; use EB_PSQL=infra/patroni/psql.sh and the multi-host OCTOPUS_PG_CONN):
#           | patroni-kill (SIGKILL the leader's container; Patroni promotes the sync standby after the leader lease)
#           | patroni-freeze:<s> (freeze the leader for s seconds: a hang or partition, not a crash; tests fencing)
#           | db-freeze:<s> (freeze both database nodes for s seconds: an outage no failover can mend)
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
dotnet "$BIN" run "$@" --Name="$NAME" --ResultsDirectory="$OUT/$NAME" --Metrics:ConsoleProgress=false > "$LOG" 2>&1 &
BENCH=$!
until grep -q " run: " "$LOG" 2>/dev/null; do kill -0 $BENCH 2>/dev/null || break; sleep 0.5; done
sleep "$AT"
{
  echo "$(date +%T.%3N) drill: $ACTION"
  case "$ACTION" in
    none) ;;
    kill:*) i=${ACTION#kill:}; pid=$(sed -n "${i}p" "$STATE/pids"); kill -KILL "$pid" && echo "killed d$i (pid $pid)" ;;
    term:*) i=${ACTION#term:}; pid=$(sed -n "${i}p" "$STATE/pids"); kill -TERM "$pid" && echo "stopped d$i (pid $pid)" ;;
    add) infra/octopus/add.sh ;;
    pgfail) infra/pg-standby/failover.sh ;;
    patroni-kill) l=$(infra/patroni/leader.sh); podman kill eb-$l >/dev/null && echo "killed leader $l"
      for _ in $(seq 1 120); do n=$(infra/patroni/leader.sh) && [[ $n != "$l" ]] && { echo "$(date +%T.%3N) new leader $n"; break; }; sleep 0.5; done ;;
    patroni-freeze:*) s=${ACTION#patroni-freeze:}; l=$(infra/patroni/leader.sh); podman pause eb-$l >/dev/null && echo "froze leader $l for ${s}s"
      for _ in $(seq 1 $((s * 2))); do n=$(curl -s --max-time 1 127.0.0.1:$((8007 + ${l#pat} % 2 + 1))/cluster | python3 -c 'import json,sys; print(next((m["name"] for m in json.load(sys.stdin)["members"] if m["role"]=="leader"), ""))' 2>/dev/null); [[ -n $n && $n != "$l" ]] && { echo "$(date +%T.%3N) new leader $n"; break; }; sleep 0.5; done
      sleep "$s"; podman unpause eb-$l >/dev/null; echo "$(date +%T.%3N) unfroze $l" ;;
    db-freeze:*) s=${ACTION#db-freeze:}; podman pause eb-pat1 eb-pat2 >/dev/null && echo "froze both database nodes for ${s}s"; sleep "$s"; podman unpause eb-pat1 eb-pat2 >/dev/null; echo "$(date +%T.%3N) unfroze" ;;
    mqtt-restart) podman restart -t 1 eb-mosquitto >/dev/null; echo "$(date +%T.%3N) restarted the MQTT broker" ;;
    dispatcher-db-cut:*) s=$(cut -d: -f3 <<<"$ACTION"); kill -STOP $PROXY && echo "cut dispatcher $(cut -d: -f2 <<<"$ACTION") from the database for ${s}s"; sleep "$s"; kill -CONT $PROXY; echo "$(date +%T.%3N) reconnected" ;;
  esac
} > "$OUT/$NAME.drill.log" 2>&1
wait $BENCH
rc=$?
[[ -n $PROXY ]] && kill $PROXY 2>/dev/null
echo "$(date +%T) $NAME -> exit $rc"
