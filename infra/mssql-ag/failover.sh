#!/usr/bin/env bash
# Manual failover (CLUSTER_TYPE = NONE has no automatic one), as an operator or a cluster manager would do it:
# promote a synchronized secondary. Planned (the old primary alive): it is demoted first. Forced (it is dead):
# FORCE_FAILOVER_ALLOW_DATA_LOSS on the target, which with synchronous commit and
# REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT loses nothing acknowledged. Then the remaining secondaries follow the
# new primary (data movement resumed), and $STATE/primary records it (the listener routes only there).
# usage: failover.sh [to-replica]
set -uo pipefail
cd "$(dirname "$0")"; source ./env.sh
TO=${1:-}
OLD=$(cat "$STATE/primary" 2>/dev/null)
ALIVE=""; [[ -n $OLD ]] && [[ $(role "$OLD") == PRIMARY ]] && ALIVE=1
if [[ -z $TO ]]; then
  # a running secondary whose databases are all SYNCHRONIZED
  for r in $(secondaries "$OLD"); do
    running "$r" || continue
    n=$(sqlq "$r" -Q "SET NOCOUNT ON; SELECT COUNT(*) FROM sys.dm_hadr_database_replica_states WHERE is_local = 1 AND synchronization_state_desc <> 'SYNCHRONIZED'" 2>/dev/null | tr -d '\r ' | head -1)
    [[ $n == 0 ]] && { TO=$r; break; }
  done
fi
[[ -z $TO ]] && { echo "no synchronized secondary to fail over to" >&2; exit 1; }
[[ -n $ALIVE ]] && sqlq "$OLD" -Q "ALTER AVAILABILITY GROUP ag SET (ROLE = SECONDARY)" >/dev/null
echo "$TO" > "$STATE/primary"   # the listener stops routing to the old primary at once
sqlq "$TO" -Q "ALTER AVAILABILITY GROUP ag FORCE_FAILOVER_ALLOW_DATA_LOSS" >/dev/null || { echo "failover to $TO failed" >&2; exit 1; }
echo "$(date +%T.%3N) $TO is primary (was $OLD, $([[ -n $ALIVE ]] && echo planned || echo "forced: $OLD unreachable"))"
# the other secondaries (and a demoted old primary) follow the new primary. A database that sees the failover is
# suspended ("failover from partner") and needs one RESUME. One that does not (it keeps reconnecting to the old
# primary, NOT SYNCHRONIZING, not suspended) is suspended by us after 5 s, then resumed: that re-attaches it. Without a
# cluster manager (CLUSTER_TYPE = NONE) nobody else drives this.
declare -A since restarted
t0=$(date +%s)
while (( $(date +%s) - t0 < 150 )); do
  pending=0
  for r in $(secondaries "$TO"); do
    running "$r" || continue
    [[ $r == "$OLD" && -z $ALIVE ]] && continue
    while read -r db st sus; do
      [[ -z $db || $st == SYNCHRONIZED ]] && continue
      pending=1; k="$r/$db"
      if (( $(date +%s) - t0 >= 20 )) && [[ -z ${restarted[$r]:-} ]]; then
        # still following the old primary 20 s on: restart that replica (its databases come back suspended by the
        # failover and are resumed above on the next rounds)
        restarted[$r]=1; echo "$(date +%T.%3N) $r still follows the old primary: restarting it"
        podman restart -t 10 "eb-$r" >/dev/null 2>&1
        for _ in $(seq 1 120); do sqlq "$r" -Q "SELECT 1" >/dev/null 2>&1 && break; sleep 0.5; done
        break
      elif [[ $sus == 1 ]]; then
        sqlq "$r" -Q "ALTER DATABASE [$db] SET HADR RESUME" >/dev/null 2>&1; since[$k]=$(date +%s)
      elif (( $(date +%s) - ${since[$k]:-$t0} >= 5 )); then
        sqlq "$r" -Q "ALTER DATABASE [$db] SET HADR SUSPEND" >/dev/null 2>&1; since[$k]=$(date +%s)
      fi
    done < <(sqlq "$r" -Q "SET NOCOUNT ON; SELECT DB_NAME(database_id), REPLACE(synchronization_state_desc, ' ', '_'), CAST(is_suspended AS int) FROM sys.dm_hadr_database_replica_states WHERE is_local = 1" 2>/dev/null | tr -d '\r')
  done
  [[ $pending == 0 ]] && { echo "$(date +%T.%3N) remaining secondaries SYNCHRONIZED"; exit 0; }
  sleep 0.5
done
echo "$(date +%T.%3N) some secondary not synchronized after 150 s" >&2
