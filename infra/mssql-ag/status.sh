#!/usr/bin/env bash
# roles, database synchronization states, and what the listener's health checks see
cd "$(dirname "$0")"; source ./env.sh
for r in "${REPLICAS[@]}"; do
  if ! podman container exists "eb-$r" || [[ $(podman inspect -f '{{.State.Running}}' "eb-$r") != true ]]; then echo "$r: down"; continue; fi
  echo "$r: $(role "$r") | $(sqlq "$r" -Q "SET NOCOUNT ON; SELECT STRING_AGG(CAST(DB_NAME(database_id) + '=' + synchronization_state_desc + '/' + database_state_desc AS nvarchar(200)) COLLATE DATABASE_DEFAULT, ' ') FROM sys.dm_hadr_database_replica_states WHERE is_local = 1" 2>/dev/null | tr -d '\r') | check $(curl -s -o /dev/null -w '%{http_code}' "127.0.0.1:${CHKPORT[$r]}/primary")"
done
