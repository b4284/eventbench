#!/usr/bin/env bash
cd /home/b4283/benchmark
O=results/matrix-27-sqlserver
row() { d=$(command ls -d $O/$1/*/ | tail -1); g() { grep "^| $1 |" $d/summary.md | cut -d'|' -f3 | tr -d ' '; }
  echo "$1 | disp_cores $(g octopus_dispatcher_cores) | mssql% $(g ctr_eb_mssql_cpu_pct) | pg% $(g ctr_eb_postgres_cpu_pct) | host% $(g host_cpu_pct)"; }
MS=(--Broker:AdminDatabase=sqlserver "--Broker:AdminConnectionString=Server=127.0.0.1,1433;Database=octopus;User Id=sa;Password=EventBench#2026;TrustServerCertificate=true")
for r in a b; do
  OCTOPUS_BACKEND=mssql scripts/perf-pgss.sh $O M8$r-mssql /home/b4283/octopus --Store:Type=inmemory "${MS[@]}" | head -1; row M8$r-mssql
  podman exec eb-mssql /opt/mssql-tools18/bin/sqlcmd -C -S 127.0.0.1 -U sa -P 'EventBench#2026' -d octopus -W -s '|' -Q "SET NOCOUNT ON; SELECT TOP 4 OBJECT_NAME(object_id, database_id) AS p, execution_count AS calls, CAST(total_worker_time/1e6 AS decimal(9,1)) AS cpu_s, CAST(total_elapsed_time/1e3/NULLIF(execution_count,0) AS decimal(9,2)) AS ms_per_call FROM sys.dm_exec_procedure_stats WHERE database_id = DB_ID('octopus') ORDER BY total_worker_time DESC" | grep -v "^-"
  scripts/perf-pgss.sh $O P8$r-pg /home/b4283/octopus --Store:Type=inmemory | head -1; row P8$r-pg; echo
done
echo MS8-DONE
