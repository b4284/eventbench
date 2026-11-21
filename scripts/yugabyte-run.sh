#!/usr/bin/env bash
cd /home/b4283/benchmark
O=results/matrix-26-yugabyte
row() { d=$(command ls -d $O/$1/*/ | tail -1); g() { grep "^| $1 |" $d/summary.md | cut -d'|' -f3 | tr -d ' '; }
  echo "$1 | disp_cores $(g octopus_dispatcher_cores) | yb% $(g ctr_eb_yb_cpu_pct) | pg% $(g ctr_eb_postgres_cpu_pct) | mosquitto% $(g ctr_eb_mosquitto_cpu_pct) | host% $(g host_cpu_pct)"; grep -E "^\| [a-z_]+ \| \*\*FAIL" $d/summary.md | cut -c1-90; }
YB=(--Broker:AdminConnectionString="Host=127.0.0.1;Port=5433;Database=bench;Username=bench;Password=bench;Maximum Pool Size=4;Command Timeout=600")
EB_PSQL="podman exec -i eb-yb bin/ysqlsh -h 2ef515023df0 -U bench -d bench -v ON_ERROR_STOP=1 -q" OCTOPUS_PG_CONN="host=127.0.0.1 port=5433 user=bench password=bench dbname=bench"   scripts/perf-pgss.sh $O Y1-yb-16k /home/b4283/octopus --Store:Type=inmemory "${YB[@]}" | head -2; row Y1-yb-16k; echo
scripts/perf-pgss.sh $O R1-pg-16k /home/b4283/octopus --Store:Type=inmemory | head -2; row R1-pg-16k
echo YB1-DONE
