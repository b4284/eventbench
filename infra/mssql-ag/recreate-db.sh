#!/usr/bin/env bash
# (Re)creates a database inside the availability group: removed from the AG and dropped on both replicas, created on
# the primary (FULL recovery, RCSI, DELAYED_DURABILITY = $MSSQL_DELAYED_DURABILITY, default DISABLED: durable), for
# "octopus" with the In-Memory OLTP filegroup ready (Octopus's In-Memory variant then skips that step), a full
# backup to NUL (starts the log chain), added to the AG (automatic seeding), and waits until the secondary is
# SYNCHRONIZED.  usage: recreate-db.sh <db>
set -euo pipefail
cd "$(dirname "$0")"; source ./env.sh
DB=${1:?db}
P=$(primary) || { echo "no primary" >&2; exit 1; }
S=$(secondaries "$P")
sqlq "$P" -Q "IF EXISTS (SELECT 1 FROM sys.availability_databases_cluster WHERE database_name = '$DB') ALTER AVAILABILITY GROUP ag REMOVE DATABASE [$DB];" >/dev/null
sqlq "$P" -Q "IF DB_ID('$DB') IS NOT NULL BEGIN ALTER DATABASE [$DB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$DB]; END" >/dev/null
for s in $S; do
  for _ in $(seq 1 30); do sqlq "$s" -Q "IF DB_ID('$DB') IS NOT NULL DROP DATABASE [$DB]" >/dev/null 2>&1 && break; sleep 1; done
done
SIZE=$([[ $DB == octopus ]] && echo 2GB || echo 4GB)
IM=""
[[ $DB == octopus && ${MSSQL_INMEMORY:-1} == 1 ]] && IM="ALTER DATABASE [$DB] ADD FILEGROUP [${DB}_imfg] CONTAINS MEMORY_OPTIMIZED_DATA;
  ALTER DATABASE [$DB] ADD FILE (NAME = N'${DB}_imfg', FILENAME = N'/var/opt/mssql/data/${DB}_imfg') TO FILEGROUP [${DB}_imfg];
  ALTER DATABASE [$DB] SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = ON;"
sqlq "$P" -Q "CREATE DATABASE [$DB]
    ON (NAME = ${DB}_data, FILENAME = '/var/opt/mssql/data/$DB.mdf', SIZE = $SIZE, FILEGROWTH = 1GB)
    LOG ON (NAME = ${DB}_log, FILENAME = '/var/opt/mssql/data/$DB.ldf', SIZE = 4GB, FILEGROWTH = 1GB);
  ALTER DATABASE [$DB] SET RECOVERY FULL;
  ALTER DATABASE [$DB] SET READ_COMMITTED_SNAPSHOT ON WITH ROLLBACK IMMEDIATE;
  ALTER DATABASE [$DB] SET ALLOW_SNAPSHOT_ISOLATION ON;
  ALTER DATABASE [$DB] SET DELAYED_DURABILITY = ${MSSQL_DELAYED_DURABILITY:-DISABLED};
  $IM
  BACKUP DATABASE [$DB] TO DISK = N'NUL';
  ALTER AVAILABILITY GROUP ag ADD DATABASE [$DB];" >/dev/null
for s in $S; do
  ok=""
  for _ in $(seq 1 240); do
    st=$(sqlq "$s" -Q "SET NOCOUNT ON; SELECT synchronization_state_desc FROM sys.dm_hadr_database_replica_states WHERE is_local = 1 AND database_id = DB_ID('$DB')" 2>/dev/null | tr -d '\r ' | head -1)
    [[ $st == SYNCHRONIZED ]] && { ok=1; break; }
    sleep 0.5
  done
  [[ -n $ok ]] || { echo "$DB: secondary $s not synchronized (state '$st')" >&2; exit 1; }
done
echo "$DB: in the AG, $(echo $S) SYNCHRONIZED (primary $P)"
