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
if pgrep -f "^dotnet .*eventbench.dll run" >/dev/null; then echo "another eventbench run is active; aborting" >&2; exit 3; fi
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
# network over the production window (common start .. start + duration): this host's eth0 and each shard host's en0
DUR=$(printf '%s\n' "$@" | sed -n 's/^--Workload:DurationSeconds=//p' | tail -1); DUR=${DUR:-60}
net_here() { awk '{printf "%s ", $1}' /sys/class/net/eth0/statistics/rx_bytes /sys/class/net/eth0/statistics/tx_bytes; }
net_mac() { ssh -o BatchMode=yes "$1" "netstat -ibn -I en0 | awk 'NR==2{print \$7, \$10}'"; }
(
  sleep $(( (START - $(date +%s%3N)) / 1000 + 5 ))   # 5 s into production
  a0=$(net_here); declare -A m0; for h in "${HOSTS[@]}"; do m0[$h]=$(net_mac "$h"); done; t0=$(date +%s)
  sleep $(( DUR - 10 ))
  a1=$(net_here); declare -A m1; for h in "${HOSTS[@]}"; do m1[$h]=$(net_mac "$h"); done; t1=$(date +%s)
  dt=$(( t1 - t0 ))
  read -r r0 x0 <<<"$a0"; read -r r1 x1 <<<"$a1"
  echo "network over ${dt} s of production (MB/s, 1 GbE ~ 117 MB/s each way):"
  echo "  laptop eth0: in $(( (r1 - r0) / dt / 1000000 )), out $(( (x1 - x0) / dt / 1000000 ))"
  for h in "${HOSTS[@]}"; do read -r i0 o0 <<<"${m0[$h]}"; read -r i1 o1 <<<"${m1[$h]}"
    echo "  $h en0: in $(( (i1 - i0) / dt / 1000000 )), out $(( (o1 - o0) / dt / 1000000 ))"; done
) > "$OUT/$NAME.net.log" 2>&1 &
NETS=$!
rc=0
for p in "${PIDS[@]}"; do wait "$p" || rc=$?; done
wait $NETS 2>/dev/null
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
cat "$OUT/$NAME.net.log"
echo "$(date +%T) $NAME -> exit $rc"
