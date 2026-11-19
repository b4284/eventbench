#!/usr/bin/env bash
# Runs a set of variants N times each, interleaved (rep1: v1..vN, rep2: v1..vN, ...) so slow drift in
# the database (WAL, autovacuum, cache) spreads evenly across variants.
#
# usage: scripts/run-matrix.sh <out-dir> <reps> <base-config> [<extra -c/--Key=value args for all runs>] -- \
#            "<name>|<args...>" "<name>|<args...>" ...
# example:
#   scripts/run-matrix.sh results/matrix 3 configs/pgmq-fifo.json -- \
#     "fifo-base|" "fifo-txack|--Worker:AckMode=InTransaction"
set -uo pipefail
cd "$(dirname "$0")/.."

OUT=$1; REPS=$2; BASE=$3; shift 3
COMMON=()
while [[ $# -gt 0 && "$1" != "--" ]]; do COMMON+=("$1"); shift; done
shift || true
VARIANTS=("$@")

# EB_BIN: run a pre-built copy (lets you keep editing/building the tree while a matrix runs).
if [[ -n "${EB_BIN:-}" ]]; then BIN=$EB_BIN
else BIN=src/EventBench.Cli/bin/Release/net10.0/eventbench.dll; dotnet build -c Release -v q >/dev/null || exit 2; fi
mkdir -p "$OUT"

for rep in $(seq 1 "$REPS"); do
  for v in "${VARIANTS[@]}"; do
    name=${v%%|*}; args=${v#*|}
    echo "$(date +%T) rep $rep/$REPS  $name  $args"
    # shellcheck disable=SC2086
    dotnet "$BIN" run -c "$BASE" "${COMMON[@]}" $args \
      --Name="$name" --ResultsDirectory="$OUT/$name" --Metrics:ConsoleProgress=false \
      > "$OUT/$name.rep$rep.log" 2>&1
    rc=$?
    echo "$(date +%T)   -> exit $rc ($( [[ $rc == 0 ]] && echo PASS || echo FAIL/ERROR ))"
    sleep 5   # let autovacuum/checkpointer settle between runs
  done
done
echo "$(date +%T) done"
