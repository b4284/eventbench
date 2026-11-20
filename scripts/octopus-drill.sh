#!/usr/bin/env bash
# Octopus failure drill: a fresh cluster of N dispatchers, a benchmark run, and one failure 30 s into the run.
# The run then finishes and drains, and the oracle checks the end state (every event applied once, in order).
#
# usage: scripts/octopus-drill.sh <out-dir> <name> <dispatchers> <action> [benchmark args...]
#   action: none | kill:<i> (SIGKILL dispatcher i) | term:<i> (SIGTERM: planned stop) | add (one more dispatcher)
#           | pgfail (kill the Postgres primary, promote the synchronous standby; needs infra/pg-standby up + sync on)
# env: DRILL_AT (seconds into the run, default 30); OCTOPUS_PG_CONN for the dispatchers (multi-host for pgfail)
set -uo pipefail
cd "$(dirname "$0")/.."
OUT=$1; NAME=$2; N=$3; ACTION=$4; shift 4
AT=${DRILL_AT:-30}
BIN=src/EventBench.Cli/bin/Release/net10.0/eventbench.dll
STATE=~/.cache/eventbench/octopus
mkdir -p "$OUT"
if pgrep -f "eventbench.dll run" >/dev/null; then echo "another eventbench run is active; aborting" >&2; exit 3; fi
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
  esac
} > "$OUT/$NAME.drill.log" 2>&1
wait $BENCH
rc=$?
echo "$(date +%T) $NAME -> exit $rc"
