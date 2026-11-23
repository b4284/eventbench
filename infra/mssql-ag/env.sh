# shared by the infra/mssql-ag scripts: SQL Server 2025 replicas on the host network (AG_REPLICAS, default 3)
#   eb-ag1: SQL 1433, mirroring endpoint 5022, role check http 8201
#   eb-ag2: SQL 1436, endpoint 5023, check 8202;  eb-ag3: SQL 1438, endpoint 5024, check 8203
#   eb-ag-haproxy: the "listener", 127.0.0.1:14330 -> the replica whose role check answers /primary
#   $STATE/primary: the replica the operator (failover.sh) made primary: the cluster manager's word, as WSFC's or
#   Pacemaker's (or Patroni's etcd key); a replica that merely believes it is primary is not routed to.
PW=${MSSQL_SA_PASSWORD:-EventBench#2026}
IMAGE=${MSSQL_IMAGE:-mcr.microsoft.com/mssql/server:2025-CU11-ubuntu-24.04}
STATE=~/.cache/eventbench/mssql-ag
ALL_REPLICAS=(ag1 ag2 ag3)
REPLICAS=("${ALL_REPLICAS[@]:0:${AG_REPLICAS:-$(cat "$STATE/replicas" 2>/dev/null || echo 3)}}")
declare -A SQLPORT=([ag1]=1433 [ag2]=1436 [ag3]=1438) EPPORT=([ag1]=5022 [ag2]=5023 [ag3]=5024) CHKPORT=([ag1]=8201 [ag2]=8202 [ag3]=8203)
LISTENER=127.0.0.1,14330
# sqlq <replica> <query...>: sqlcmd against that replica (run inside its own container)
sqlq() { local r=$1; shift; podman exec "eb-$r" /opt/mssql-tools18/bin/sqlcmd -C -S "127.0.0.1,${SQLPORT[$r]}" -U sa -P "$PW" -b -h -1 -W "$@"; }
running() { [[ "$(podman inspect -f '{{.State.Running}}' "eb-$1" 2>/dev/null)" == true ]]; }
role() { running "$1" && sqlq "$1" -Q "SET NOCOUNT ON; SELECT role_desc FROM sys.dm_hadr_availability_replica_states WHERE is_local = 1" 2>/dev/null | tr -d '\r ' | head -1; }
# the primary: the recorded one if it says so, else any replica that says so
primary() {
  local p; p=$(cat "$STATE/primary" 2>/dev/null)
  [[ -n $p && $(role "$p") == PRIMARY ]] && { echo "$p"; return 0; }
  for r in "${REPLICAS[@]}"; do [[ $(role "$r") == PRIMARY ]] && { echo "$r"; return 0; }; done; return 1
}
secondaries() { local p=$1; for r in "${REPLICAS[@]}"; do [[ $r != "$p" ]] && echo "$r"; done; }
replica_spec() { echo "N'$1' WITH (ENDPOINT_URL = N'tcp://127.0.0.1:${EPPORT[$1]}', AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
  FAILOVER_MODE = MANUAL, SEEDING_MODE = AUTOMATIC, SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL))"; }
