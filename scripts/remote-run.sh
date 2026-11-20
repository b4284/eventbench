#!/usr/bin/env bash
# A benchmark run with the benchmark process (producers + workers) on another machine, and the database,
# PgBouncer, MQTT broker and Octopus dispatchers here. CPU of this side is sampled here every 3 s.
#
# usage: scripts/remote-run.sh <out-dir> <name> <dispatchers> [benchmark args...]
# env: EB_REMOTE (ssh host, default leedemac-mini), EB_REMOTE_DIR (default eventbench)
set -uo pipefail
cd "$(dirname "$0")/.."
OUT=$1; NAME=$2; N=$3; shift 3
R=${EB_REMOTE:-leedemac-mini}; RD=${EB_REMOTE_DIR:-eventbench}
STATE=~/.cache/eventbench/octopus
mkdir -p "$OUT"
EB_MQTT=${EB_MQTT:-mosquitto} EB_MQTT_BIND=0.0.0.0 infra/octopus/up.sh "$N" > "$OUT/$NAME.pre.log" 2>&1 || { echo "cluster did not start"; exit 2; }
rsync -a configs/ "$R:$RD/configs/"
SAMPLES="$OUT/$NAME.local-cpu.tsv"
: > "$SAMPLES"
(
  prev_host=$(awk '/^cpu /{print $2+$3+$4+$6+$7+$8, $2+$3+$4+$5+$6+$7+$8}' /proc/stat)
  declare -A prev
  while sleep 3; do
    ts=$(date +%s)
    # CPU time (clock ticks) per process name: deltas give cores (podman stats reports averages since start)
    awk '$2=="(postgres)"{p+=$14+$15} $2=="(pgbouncer)"{b+=$14+$15} $2=="(mosquitto)"{m+=$14+$15}
         END{printf "%s\tpostgres_ticks\t%d\n%s\tpgbouncer_ticks\t%d\n%s\tmosquitto_ticks\t%d\n", ts, p, ts, b, ts, m}' \
        ts="$ts" /proc/[0-9]*/stat 2>/dev/null >> "$SAMPLES"
    d=0; for p in $(cat "$STATE/pids" 2>/dev/null); do [[ -r /proc/$p/stat ]] && d=$((d + $(awk '{print $14+$15}' /proc/$p/stat))); done
    printf '%s\tdispatchers_ticks\t%s\n' "$ts" "$d" >> "$SAMPLES"
    printf '%s\tnet_rx_bytes\t%s\n%s\tnet_tx_bytes\t%s\n' "$ts" "$(cat /sys/class/net/eth0/statistics/rx_bytes)" "$ts" "$(cat /sys/class/net/eth0/statistics/tx_bytes)" >> "$SAMPLES"
    cur_host=$(awk '/^cpu /{print $2+$3+$4+$6+$7+$8, $2+$3+$4+$5+$6+$7+$8}' /proc/stat)
    read -r b0 t0 <<<"$prev_host"; read -r b1 t1 <<<"$cur_host"
    printf '%s\thost_pct\t%s\n' "$ts" "$(( (b1 - b0) * 100 / (t1 - t0 > 0 ? t1 - t0 : 1) ))" >> "$SAMPLES"
    prev_host=$cur_host
  done
) &
SAMPLER=$!
# the remote overlay goes last among the config files (they apply in order; --Key=value overrides apply after all)
ARGS=$(printf ' %q' "$@")
ssh -o BatchMode=yes "$R" "cd $RD && ./bin/eventbench run$ARGS -c configs/overlay-remote-app.json --Name=$NAME --ResultsDirectory=results/$NAME --Metrics:ConsoleProgress=false" > "$OUT/$NAME.log" 2>&1
rc=$?
kill $SAMPLER 2>/dev/null; wait $SAMPLER 2>/dev/null
rsync -a "$R:$RD/results/$NAME/" "$OUT/$NAME/"
echo "$(date +%T) $NAME -> exit $rc"
