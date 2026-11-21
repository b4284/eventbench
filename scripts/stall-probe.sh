#!/usr/bin/env bash
# Samples the Patroni leader every 15 s during a drill: non-idle sessions (wait event, transaction age, query) and
# blocked lock waits. usage: scripts/stall-probe.sh <out-file> <seconds>
OUT=$1; DUR=${2:-300}
end=$(( $(date +%s) + DUR ))
while [[ $(date +%s) -lt $end ]]; do
  {
    echo "=== $(date +%T)"
    infra/patroni/psql.sh -At -F' | ' -c "select pid, state, coalesce(wait_event_type,'-')||':'||coalesce(wait_event,'-'), now()-xact_start, client_port, left(regexp_replace(query,'\s+',' ','g'),70)
        from pg_stat_activity where backend_type='client backend' and pid <> pg_backend_pid()
         and (state <> 'idle' or xact_start is not null) order by xact_start nulls last limit 25" 2>&1
    echo "--- blocked"
    infra/patroni/psql.sh -At -F' | ' -c "select a.pid, pg_blocking_pids(a.pid), a.state, left(a.query,60) from pg_stat_activity a where cardinality(pg_blocking_pids(a.pid)) > 0 limit 15" 2>&1
  } >> "$OUT"
  sleep 15
done
