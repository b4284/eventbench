#!/usr/bin/env bash
# Scale out: start one more dispatcher (member d<k>) while the cluster runs.
set -euo pipefail
STATE=~/.cache/eventbench/octopus
OCTO=${OCTOPUS_REPO:-$(cd "$(dirname "$0")/../../.." && pwd)/octopus}
k=$(( $(wc -l < "$STATE/pids") + 1 ))
env OCTOPUS_MEMBER_ID="d$k" OCTOPUS_NS=octopus OCTOPUS_SCHEMA=octopus \
    OCTOPUS_PG="host=127.0.0.1 port=5432 user=bench password=bench dbname=bench" OCTOPUS_PG_POOL="${OCTOPUS_PG_POOL:-32}" \
    OCTOPUS_APPLY_HOOK=public.eb_octopus_apply OCTOPUS_LOAD_HOOK=public.eb_octopus_load RUST_LOG="${RUST_LOG:-info}" ${OCTOPUSD_ENV:-} \
    nohup "$OCTO/target/release/octopusd" >"$STATE/d$k.log" 2>&1 &
echo $! >> "$STATE/pids"
echo "started dispatcher d$k (pid $!)"
