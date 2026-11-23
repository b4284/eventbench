#!/usr/bin/env bash
# The old primary of a forced failover comes back: it may still believe it is primary (with CLUSTER_TYPE = NONE nothing
# tells it otherwise), and its log may run past the new primary's (unacknowledged work). The usual repair, done here:
# drop its copy of the AG and its databases, remove and re-add it as a replica on the primary, join, and let automatic
# seeding copy the databases again. The listener never routed to it ($STATE/primary).  usage: rejoin.sh <replica>
set -uo pipefail
cd "$(dirname "$0")"; source ./env.sh
R=${1:?replica}
for _ in $(seq 1 120); do sqlq "$R" -Q "SELECT 1" >/dev/null 2>&1 && break; sleep 0.5; done
P=$(cat "$STATE/primary")
echo "$(date +%T.%3N) $R is back (believes: $(role "$R")); rebuilding it as a secondary of $P"
sqlq "$R" -Q "DROP AVAILABILITY GROUP ag" >/dev/null 2>&1 || true
for db in bench octopus; do sqlq "$R" -Q "IF DB_ID('$db') IS NOT NULL DROP DATABASE [$db]" >/dev/null 2>&1 || true; done
sqlq "$P" -Q "ALTER AVAILABILITY GROUP ag REMOVE REPLICA ON N'$R'" >/dev/null 2>&1 || true
sqlq "$P" -Q "ALTER AVAILABILITY GROUP ag ADD REPLICA ON $(replica_spec "$R")" >/dev/null
sqlq "$R" -Q "ALTER AVAILABILITY GROUP ag JOIN WITH (CLUSTER_TYPE = NONE)" >/dev/null
for _ in $(seq 1 30); do sqlq "$R" -Q "ALTER AVAILABILITY GROUP ag GRANT CREATE ANY DATABASE" >/dev/null 2>&1 && break; sleep 1; done
for _ in $(seq 1 600); do
  n=$(sqlq "$R" -Q "SET NOCOUNT ON; SELECT COUNT(*) FROM sys.dm_hadr_database_replica_states WHERE is_local = 1 AND synchronization_state_desc = 'SYNCHRONIZED'" 2>/dev/null | tr -d '\r ' | head -1)
  [[ ${n:-0} -ge 2 ]] && { echo "$(date +%T.%3N) $R rejoined as $(role "$R"), databases seeded and SYNCHRONIZED"; exit 0; }
  sleep 0.5
done
echo "$(date +%T.%3N) $R: databases not synchronized after 300 s" >&2; exit 1
