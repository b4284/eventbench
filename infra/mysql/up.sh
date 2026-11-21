#!/usr/bin/env bash
# Starts MySQL 8.4 LTS for Octopus (container eb-mysql, host network, 127.0.0.1:3306 and the LAN; root / $MYSQL_ROOT_PASSWORD).
# Databases: octopus (benchmark runs; recreated by infra/octopus/up.sh) and octopus_test (the backend's tests).
# Durability as in the other labs' comparisons: best effort (the redo log written at commit, flushed about once a
# second: innodb_flush_log_at_trx_commit=2, sync_binlog=0). The binary log stays on (GTIDs) for the replication drill.
# READ COMMITTED by default: no gap locks.
# Tunables via env: MYSQL_IMAGE, MYSQL_BUFFER_POOL (default 4G), MYSQL_BINLOG=0 (no binary log: no replication,
# no group commit through it; to measure what it costs, matrix-31)
set -euo pipefail
NAME=${MYSQL_CONTAINER:-eb-mysql}
IMAGE=${MYSQL_IMAGE:-docker.io/library/mysql:8.4.13}
PASSWORD=${MYSQL_ROOT_PASSWORD:-bench}
if podman container exists "$NAME"; then echo "container $NAME already exists (podman rm -f $NAME to recreate)"; exit 0; fi
podman run -d --name "$NAME" --network host -e MYSQL_ROOT_PASSWORD="$PASSWORD" -e MYSQL_ROOT_HOST=% \
  -v "$NAME-data:/var/lib/mysql" "$IMAGE" \
  --bind-address=0.0.0.0 --port=3306 --mysqlx=OFF \
  --max-connections=1000 --max-allowed-packet=256M \
  --innodb-buffer-pool-size="${MYSQL_BUFFER_POOL:-4G}" --innodb-redo-log-capacity=4G \
  --innodb-flush-log-at-trx-commit=2 --sync-binlog=0 \
  --transaction-isolation=READ-COMMITTED \
  --character-set-server=utf8mb4 --collation-server=utf8mb4_bin \
  $([[ ${MYSQL_BINLOG:-1} == 0 ]] && echo --disable-log-bin || echo --gtid-mode=ON --enforce-gtid-consistency=ON --binlog-expire-logs-seconds=3600) \
  --log-bin-trust-function-creators=1 --skip-name-resolve >/dev/null
sql() { podman exec -i "$NAME" mysql -uroot -p"$PASSWORD" -N -B "$@" 2>/dev/null; }
echo -n "waiting for mysqld"
for _ in $(seq 1 120); do sql -e "SELECT 1" >/dev/null && { echo " ok"; break; }; echo -n "."; sleep 1; done
sql -e "CREATE DATABASE IF NOT EXISTS octopus_test; CREATE DATABASE IF NOT EXISTS octopus;"
sql -e "SELECT VERSION(), @@transaction_isolation, @@innodb_flush_log_at_trx_commit, @@sync_binlog, @@gtid_mode"
