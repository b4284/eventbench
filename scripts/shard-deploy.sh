#!/usr/bin/env bash
# Builds the benchmark as a self-contained macOS arm64 binary and copies it to the shard hosts (~/eventbench).
# usage: EB_SHARD_HOSTS="m4mini m1mini" scripts/shard-deploy.sh
set -euo pipefail
cd "$(dirname "$0")/.."
read -r -a HOSTS <<<"${EB_SHARD_HOSTS:-m4mini}"
OUTDIR=$(mktemp -d)
dotnet publish src/EventBench.Cli -c Release -r osx-arm64 --self-contained true -p:PublishSingleFile=false -o "$OUTDIR/bin" >/dev/null
for h in "${HOSTS[@]}"; do
  ssh -o BatchMode=yes "$h" 'mkdir -p eventbench/results'
  rsync -a --delete "$OUTDIR/bin/" "$h:eventbench/bin/"
  rsync -a configs/ "$h:eventbench/configs/"
  echo "$h: $(ssh -o BatchMode=yes "$h" 'du -sh eventbench/bin | cut -f1') deployed"
done
rm -rf "$OUTDIR"
