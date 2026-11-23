#!/usr/bin/env bash
# A SQL Server availability group of AG_REPLICAS replicas (default 3; 2 shows the 1+1 problem) on the laptop (B16): synchronous commit, CLUSTER_TYPE = NONE (no
# Pacemaker, so failover is manual: infra/mssql-ag/failover.sh), REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 1
# (a commit waits for the secondary; with the secondary gone, commits stop instead of running unprotected),
# certificate-authenticated mirroring endpoints, automatic seeding, FULL recovery. Database "bench" (the
# application's writes) joins at once; "octopus" is (re)created per run by infra/mssql-ag/recreate-db.sh.
# Clients use the listener 127.0.0.1,14330 (HAProxy following the replica that reports PRIMARY).
# Stops eb-mssql (same port). env: MSSQL_MEMORY_MB (per replica, default 6144), AG_REPLICAS (default 3), AG_REQUIRED (default 1)
set -euo pipefail
cd "$(dirname "$0")"; source ./env.sh
mkdir -p "$STATE"
echo "${AG_REPLICAS:-3}" > "$STATE/replicas"; source ./env.sh
podman stop -t 10 eb-mssql >/dev/null 2>&1 || true
for r in "${ALL_REPLICAS[@]}"; do podman rm -f "eb-$r" >/dev/null 2>&1 || true; done
for r in "${REPLICAS[@]}"; do
  podman run -d --name "eb-$r" --network host --hostname "$r" \
    -e ACCEPT_EULA=Y -e MSSQL_PID=Developer -e "MSSQL_SA_PASSWORD=$PW" -e MSSQL_ENABLE_HADR=1 \
    -e "MSSQL_TCP_PORT=${SQLPORT[$r]}" -e "MSSQL_MEMORY_LIMIT_MB=${MSSQL_MEMORY_MB:-6144}" "$IMAGE" >/dev/null
done
wait_up() { for _ in $(seq 1 120); do sqlq "$1" -Q "SELECT 1" >/dev/null 2>&1 && return 0; sleep 1; done; echo "eb-$1 did not start" >&2; podman logs --tail 20 "eb-$1" >&2; exit 1; }
for r in "${REPLICAS[@]}"; do wait_up "$r"; done
# as infra/mssql/up.sh: MAXDOP 1, memory-optimized tempdb metadata (needs a restart)
for r in "${REPLICAS[@]}"; do
  sqlq "$r" -Q "EXEC sp_configure 'show advanced options', 1; RECONFIGURE; EXEC sp_configure 'max degree of parallelism', 1; RECONFIGURE;
                ALTER SERVER CONFIGURATION SET MEMORY_OPTIMIZED TEMPDB_METADATA = ON;" >/dev/null
  podman restart -t 30 "eb-$r" >/dev/null
done
for r in "${REPLICAS[@]}"; do wait_up "$r"; done
echo "replicas up: $(for r in "${REPLICAS[@]}"; do echo -n "$r=$(sqlq "$r" -Q "SET NOCOUNT ON; SELECT @@SERVERNAME" | tr -d '\r') "; done)"

# endpoints, authenticated by certificates (no domain)
for r in "${REPLICAS[@]}"; do
  sqlq "$r" -Q "CREATE MASTER KEY ENCRYPTION BY PASSWORD = '$PW';
                CREATE CERTIFICATE ${r}_cert WITH SUBJECT = '$r endpoint', EXPIRY_DATE = '2036-01-01';
                BACKUP CERTIFICATE ${r}_cert TO FILE = '/var/opt/mssql/data/${r}_cert.cer';
                CREATE ENDPOINT hadr STATE = STARTED AS TCP (LISTENER_PORT = ${EPPORT[$r]}, LISTENER_IP = ALL)
                  FOR DATABASE_MIRRORING (ROLE = ALL, AUTHENTICATION = CERTIFICATE ${r}_cert, ENCRYPTION = REQUIRED ALGORITHM AES);" >/dev/null
  podman cp "eb-$r:/var/opt/mssql/data/${r}_cert.cer" "$STATE/${r}_cert.cer"
done
for r in "${REPLICAS[@]}"; do
  for o in "${REPLICAS[@]}"; do
    [[ $o == "$r" ]] && continue
    podman cp "$STATE/${o}_cert.cer" "eb-$r:/var/opt/mssql/data/${o}_cert.cer"
    podman exec -u root "eb-$r" chown mssql "/var/opt/mssql/data/${o}_cert.cer"
    sqlq "$r" -Q "CREATE LOGIN ${o}_login WITH PASSWORD = '$PW';
                  CREATE USER ${o}_user FOR LOGIN ${o}_login;
                  CREATE CERTIFICATE ${o}_cert AUTHORIZATION ${o}_user FROM FILE = '/var/opt/mssql/data/${o}_cert.cer';
                  GRANT CONNECT ON ENDPOINT::hadr TO ${o}_login;" >/dev/null
  done
done

sqlq ag1 -Q "CREATE AVAILABILITY GROUP ag WITH (CLUSTER_TYPE = NONE, REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = ${AG_REQUIRED:-1})
             FOR REPLICA ON $(for r in "${REPLICAS[@]}"; do replica_spec "$r"; echo ,; done | sed '$d');
             ALTER AVAILABILITY GROUP ag GRANT CREATE ANY DATABASE;" >/dev/null
for r in $(secondaries ag1); do
  sqlq "$r" -Q "ALTER AVAILABILITY GROUP ag JOIN WITH (CLUSTER_TYPE = NONE)" >/dev/null
  # (right after the join the grant can fail: retried)
  for _ in $(seq 1 30); do sqlq "$r" -Q "ALTER AVAILABILITY GROUP ag GRANT CREATE ANY DATABASE" >/dev/null 2>&1 && break; sleep 1; done
done
echo ag1 > "$STATE/primary"

# the listener: a role check per replica (http 200 on /primary from the PRIMARY) and HAProxy in front
for pid in $(pgrep -f "^python3 rolecheck"); do kill "$pid"; done
for r in "${REPLICAS[@]}"; do
  nohup python3 rolecheck.py "$r" "${CHKPORT[$r]}" > "$STATE/rolecheck-$r.log" 2>&1 &
done
{
  cat <<CFG
global
    maxconn 8000
defaults
    mode tcp
    timeout connect 3s
    timeout client 30m
    timeout server 30m
listen primary
    bind 127.0.0.1:14330
    option httpchk GET /primary
    http-check expect status 200
    default-server inter 1s fall 2 rise 2 on-marked-down shutdown-sessions
CFG
  for r in "${REPLICAS[@]}"; do echo "    server $r 127.0.0.1:${SQLPORT[$r]} check port ${CHKPORT[$r]}"; done
} > "$STATE/haproxy.cfg"
podman rm -f eb-ag-haproxy >/dev/null 2>&1 || true
podman run -d --name eb-ag-haproxy --network host -v "$STATE/haproxy.cfg:/usr/local/etc/haproxy/haproxy.cfg:ro,Z" docker.io/library/haproxy:2.9 >/dev/null

./recreate-db.sh bench
./status.sh
