#!/usr/bin/env bash
# Sweeps PgBouncer default_pool_size (interleaved reps), restarting PgBouncer before every run.
# usage: scripts/pool-sweep.sh <out-dir> <reps> "<sizes>" <eventbench args...>
# example: scripts/pool-sweep.sh results/pool 3 "64 128 256" -c configs/pgmq-fifo.json -c configs/overlay-32k.json
set -uo pipefail
cd "$(dirname "$0")/.."
OUT=$1; REPS=$2; SIZES=$3; shift 3
INI=infra/pgbouncer/pgbouncer.ini
BIN=src/EventBench.Cli/bin/Release/net10.0/eventbench.dll
ORIGINAL=$(grep -E '^default_pool_size' "$INI" | awk -F= '{gsub(/ /,"",$2); print $2}')
restore() { sed -i -E "s/^default_pool_size = .*/default_pool_size = $ORIGINAL/" "$INI"; podman restart eb-pgbouncer >/dev/null; echo "restored default_pool_size = $ORIGINAL"; }
trap restore EXIT
mkdir -p "$OUT"
for rep in $(seq 1 "$REPS"); do
  for size in $SIZES; do
    sed -i -E "s/^default_pool_size = .*/default_pool_size = $size/" "$INI"
    podman restart eb-pgbouncer >/dev/null
    for _ in $(seq 1 30); do
      podman exec eb-postgres psql "host=127.0.0.1 port=6432 dbname=bench user=bench password=bench" -Atc "select 1" >/dev/null 2>&1 && break; sleep 1
    done
    name="pool-$size"
    echo "$(date +%T) rep $rep/$REPS  default_pool_size=$size"
    dotnet "$BIN" run "$@" --Name="$name" --ResultsDirectory="$OUT/$name" --Metrics:ConsoleProgress=false > "$OUT/$name.rep$rep.log" 2>&1
    echo "$(date +%T)   -> exit $?"
    sleep 5
  done
done
