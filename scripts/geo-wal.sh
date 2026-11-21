#!/usr/bin/env bash
cd /home/b4283/benchmark
export EB_MQTT=mosquitto EB_PSQL="$PWD/infra/patroni-geo/psql.sh -v ON_ERROR_STOP=1 -q" OCTOPUS_PG_CONN="host=127.0.0.1 port=6440 user=bench password=bench dbname=bench connect_timeout=2"
O=results/matrix-23-geo
P() { infra/patroni-geo/psql.sh -At "$@"; }
cfg() { podman exec geo-dc1a patronictl -c /etc/patroni/dc1a.yml edit-config "$@" --force >/dev/null; sleep 12; }
run() {  # name
  pgrep -f "/target/release/octopusd$" | xargs -r kill; sleep 1
  P -c "checkpoint" >/dev/null
  l0=$(P -c 'select pg_current_wal_lsn()'); n0=$(ssh -o BatchMode=yes m4mini "netstat -ibn -I en0 | awk 'NR==2{print \$7}'"); c0=$(awk '/^cpu /{print $2+$3+$4+$7+$8, $5+$6}' /proc/stat); t0=$(date +%s)
  DRILL_TIMEOUT=1200 scripts/octopus-drill.sh $O $1 3 none -c configs/pgmq-500groups.json -c configs/overlay-slow.json \
    -c configs/overlay-octopus-worker.json -c configs/overlay-patroni-geo.json --Store:BatchCommits=true --Store:BatchExecutors=16 \
    --Store:BatchMaxSize=32 --Workload:DrainTimeoutSeconds=180 > /dev/null 2>&1
  b=$(P -c "select pg_current_wal_lsn() - '$l0'::pg_lsn"); n1=$(ssh -o BatchMode=yes m4mini "netstat -ibn -I en0 | awk 'NR==2{print \$7}'"); c1=$(awk '/^cpu /{print $2+$3+$4+$7+$8, $5+$6}' /proc/stat); t1=$(date +%s)
  d=$(command ls -d $O/$1/*/ | tail -1)
  t=$(grep -A4 '^## Throughput' $d/summary.md | tail -1)
  ev=$(awk -F'|' '{gsub(/ /,"",$3); print $3}' <<<"$t")
  echo "$1: $(P -At -c 'show synchronous_commit') max_wal_size=$(P -At -c 'show max_wal_size') wal_compression=$(P -At -c 'show wal_compression')"
  echo "   applied $ev  steady produced/s $(awk -F'|' '{print $4}' <<<"$t")  applied/s $(awk -F'|' '{print $5}' <<<"$t")"
  python3 -c "
b=$b; e=$ev; n=$n1-$n0; dt=$t1-$t0; c0='$c0'.split(); c1='$c1'.split(); bu=float(c1[0])-float(c0[0]); idl=float(c1[1])-float(c0[1])
print(f'   WAL {b/2**30:.2f} GiB = {b/e/1024:.1f} KiB/event (exact, LSN); m4mini received {n/2**30:.2f} GiB = {n/b:.2f}x the WAL, avg {n/dt/2**20:.0f} MiB/s over {dt}s; laptop CPU busy {100*bu/(bu+idl):.0f}%')"
  grep -E "^\| e2e \|" $d/summary.md | awk -F'|' '{print "   e2e mean/p50/p99 ms", $4, $5, $7}'
}
cfg -p synchronous_commit=remote_write
run W4-sync-tuned
cfg -p synchronous_commit=local
run W5-async-tuned
cfg -p synchronous_commit=remote_write
run W6-sync-tuned
pgrep -f "/target/release/octopusd$" | xargs -r kill
echo WAL-DONE
