#!/usr/bin/env bash
# One benchmark run split across machines: this host runs shard 0 (and the database, PgBouncer, MQTT broker and
# Octopus dispatchers); each host in EB_SHARD_HOSTS runs one more shard, i.e. the producers and workers of its share
# of the groups (group id % shards). Every shard starts producing at the same wall-clock instant; shard 0 sets up
# the shared store before it. Each shard writes its own results; this script prints them side by side.
#
# usage: EB_SHARD_HOSTS="m4mini [m1mini]" scripts/shard-run.sh <out-dir> <name> <dispatchers> [benchmark args...]
# The remote hosts need the self-contained build in ~/eventbench (scripts/shard-deploy.sh).
# env: EB_SHARD_LEAD_S (seconds between launch and the common start, default 30)
set -uo pipefail
cd "$(dirname "$0")/.."
OUT=$1; NAME=$2; N=$3; shift 3
read -r -a HOSTS <<<"${EB_SHARD_HOSTS:-m4mini}"
SHARDS=$(( ${#HOSTS[@]} + 1 ))
BIN=src/EventBench.Cli/bin/Release/net10.0/eventbench.dll
STATE=~/.cache/eventbench/octopus
mkdir -p "$OUT"
if pgrep -f "eventbench.dll run" >/dev/null; then echo "another eventbench run is active; aborting" >&2; exit 3; fi
pgrep -f "/target/release/octopusd$" | xargs -r kill; sleep 1
EB_MQTT=${EB_MQTT:-mosquitto} EB_MQTT_BIND=0.0.0.0 infra/octopus/up.sh "$N" > "$OUT/$NAME.pre.log" 2>&1 || { echo "cluster did not start"; tail -3 "$OUT/$NAME.pre.log"; exit 2; }
for h in "${HOSTS[@]}"; do rsync -a configs/ "$h:eventbench/configs/"; done
START=$(( $(date +%s%3N) + ${EB_SHARD_LEAD_S:-30} * 1000 ))
COMMON=(--Workload:ShardCount="$SHARDS" --Workload:StartAtUnixMs="$START" --Metrics:ConsoleProgress=false)
timeout --signal=INT --kill-after=30 "${RUN_TIMEOUT:-900}" dotnet "$BIN" run "$@" "${COMMON[@]}" --Workload:ShardIndex=0 \
  --Name="$NAME-s0" --ResultsDirectory="$OUT/$NAME-s0" > "$OUT/$NAME-s0.log" 2>&1 &
PIDS=($!)
ARGS=$(printf ' %q' "$@" "${COMMON[@]}")
i=1
for h in "${HOSTS[@]}"; do
  # held ssh session: on macOS a process that outlives its ssh session loses the LAN (Local Network privacy)
  ssh -o BatchMode=yes -o ServerAliveInterval=10 "$h" \
    "cd eventbench && ./bin/eventbench run$ARGS -c configs/overlay-remote-app.json --Workload:ShardIndex=$i --Name=$NAME-s$i --ResultsDirectory=results/$NAME-s$i" \
    > "$OUT/$NAME-s$i.log" 2>&1 &
  PIDS+=($!); i=$((i + 1))
done
rc=0
for p in "${PIDS[@]}"; do wait "$p" || rc=$?; done
i=1
for h in "${HOSTS[@]}"; do rsync -a "$h:eventbench/results/$NAME-s$i/" "$OUT/$NAME-s$i/"; i=$((i + 1)); done
for f in "$STATE"/d*.log; do [[ -f $f ]] && cp "$f" "$OUT/$NAME.$(basename "$f")"; done
for s in $(seq 0 $((SHARDS - 1))); do
  d=$(command ls -d "$OUT/$NAME-s$s"/*/ 2>/dev/null | tail -1)
  [[ -z $d ]] && { echo "$NAME-s$s: no result (see $OUT/$NAME-s$s.log)"; continue; }
  t=$(grep -A4 '^## Throughput' "$d/summary.md" | tail -1); e=$(grep -E "^\| e2e \|" "$d/summary.md")
  fails=$(grep -cE "^\| [a-z_]+ \| \*\*FAIL" "$d/summary.md")
  echo "$NAME-s$s: produced/s $(cut -d'|' -f4 <<<"$t") applied/s $(cut -d'|' -f5 <<<"$t") e2e p50 $(cut -d'|' -f5 <<<"$e") p99 $(cut -d'|' -f7 <<<"$e") | checks failed: $fails"
done
echo "$(date +%T) $NAME -> exit $rc"
