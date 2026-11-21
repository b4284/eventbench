#!/usr/bin/env bash
cd /home/b4283/benchmark
export EB_MQTT=mosquitto EB_PSQL="$PWD/infra/patroni-geo/psql.sh -v ON_ERROR_STOP=1 -q" OCTOPUS_PG_CONN="host=127.0.0.1 port=6440 user=bench password=bench dbname=bench connect_timeout=2"
O=results/matrix-23-geo; mkdir -p $O
mode() { podman exec geo-dc1a patronictl -c /etc/patroni/dc1a.yml edit-config -p synchronous_commit=$1 --force >/dev/null; sleep 12
  echo "$(date +%T) synchronous_commit=$(infra/patroni-geo/psql.sh -At -c 'show synchronous_commit')"; }
for r in "G1-sync remote_write" "G2-async local" "G3-sync remote_write" "G4-async local"; do set -- $r
  mode $2
  pgrep -f "/target/release/octopusd$" | xargs -r kill; sleep 1
  DRILL_TIMEOUT=1200 scripts/octopus-drill.sh $O $1 3 none -c configs/pgmq-500groups.json -c configs/overlay-slow.json \
    -c configs/overlay-octopus-worker.json -c configs/overlay-patroni-geo.json --Store:BatchCommits=true --Store:BatchExecutors=16 \
    --Store:BatchMaxSize=32 --Workload:DrainTimeoutSeconds=180 > /dev/null 2>&1
  d=$(command ls -d $O/$1/*/ | tail -1)
  echo "$1: $(grep -A4 '^## Throughput' $d/summary.md | tail -1 | cut -c1-140)"
  grep -E "e2e|End-to-end" $d/summary.md | head -2 | cut -c1-160
  grep -cE "^\| [a-z_]+ \| \*\*FAIL" $d/summary.md | sed 's/^/fails: /'
done
pgrep -f "/target/release/octopusd$" | xargs -r kill
mode remote_write
echo AB-DONE
