#!/usr/bin/env bash
cd /home/b4283/benchmark
O=results/matrix-25-dispatcher-peak
row() { d=$(command ls -d $O/$1/*/ | tail -1); g() { grep "^| $1 |" $d/summary.md | cut -d'|' -f3 | tr -d ' '; }
  echo "$1 | disp_cores $(g octopus_dispatcher_cores) | mosquitto% $(g ctr_eb_mosquitto_cpu_pct) | pg% $(g ctr_eb_postgres_cpu_pct) | bench_cores $(g bench_cpu_cores) | host% $(g host_cpu_pct)"; }
for spec in "D1-16k 1 1.24" "D1-24k 1 1.86" "D1-32k 1 2.48" "D3-32k 3 2.48"; do set -- $spec
  NDISP=$2 scripts/perf-pgss.sh $O $1 /home/b4283/octopus --Store:Type=inmemory --Workload:RateMultiplier=$3 | head -2
  row $1; echo
done
echo B21-DONE
