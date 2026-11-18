#!/usr/bin/env bash
# Starts a single-node Kafka (KRaft, combined broker+controller) with podman.
#   Kafka : 127.0.0.1:9092 (PLAINTEXT)
# Large messages (~22 MB base64 JSON for 16 MB uploads) are allowed up to KAFKA_MAX_MESSAGE_BYTES.
# Independent of the `eventbench` pod (Postgres/PgBouncer); it does not touch it.
# Tunables via env: KAFKA_IMAGE, KAFKA_PORT, KAFKA_HEAP, KAFKA_MAX_MESSAGE_BYTES, KAFKA_CPUS, KAFKA_MEMORY
set -euo pipefail

NAME=${KAFKA_CONTAINER:-eb-kafka}
KAFKA_IMAGE=${KAFKA_IMAGE:-docker.io/apache/kafka:4.1.1}
KAFKA_PORT=${KAFKA_PORT:-9092}
KAFKA_HEAP=${KAFKA_HEAP:-2G}
MAX_MSG=${KAFKA_MAX_MESSAGE_BYTES:-67108864}      # 64 MB
KAFKA_CPUS=${KAFKA_CPUS:-}
KAFKA_MEMORY=${KAFKA_MEMORY:-}

limits=()
[[ -n "$KAFKA_CPUS" ]] && limits+=(--cpus "$KAFKA_CPUS")
[[ -n "$KAFKA_MEMORY" ]] && limits+=(--memory "$KAFKA_MEMORY")

if podman container exists "$NAME"; then
  echo "container $NAME already exists (run infra/kafka/down.sh first to recreate)"; exit 0
fi

podman run -d --name "$NAME" "${limits[@]}" \
  -p "127.0.0.1:${KAFKA_PORT}:9092" \
  -e KAFKA_NODE_ID=1 \
  -e KAFKA_PROCESS_ROLES=broker,controller \
  -e KAFKA_LISTENERS=PLAINTEXT://:9092,CONTROLLER://:9093 \
  -e KAFKA_ADVERTISED_LISTENERS="PLAINTEXT://127.0.0.1:${KAFKA_PORT}" \
  -e KAFKA_CONTROLLER_LISTENER_NAMES=CONTROLLER \
  -e KAFKA_LISTENER_SECURITY_PROTOCOL_MAP=CONTROLLER:PLAINTEXT,PLAINTEXT:PLAINTEXT \
  -e KAFKA_CONTROLLER_QUORUM_VOTERS=1@localhost:9093 \
  -e KAFKA_INTER_BROKER_LISTENER_NAME=PLAINTEXT \
  -e KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1 \
  -e KAFKA_OFFSETS_TOPIC_NUM_PARTITIONS=10 \
  -e KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR=1 \
  -e KAFKA_TRANSACTION_STATE_LOG_MIN_ISR=1 \
  -e KAFKA_GROUP_INITIAL_REBALANCE_DELAY_MS=0 \
  -e KAFKA_AUTO_CREATE_TOPICS_ENABLE=false \
  -e KAFKA_DELETE_TOPIC_ENABLE=true \
  -e KAFKA_NUM_PARTITIONS=1 \
  -e KAFKA_DEFAULT_REPLICATION_FACTOR=1 \
  -e KAFKA_MIN_INSYNC_REPLICAS=1 \
  -e KAFKA_UNCLEAN_LEADER_ELECTION_ENABLE=false \
  -e KAFKA_MESSAGE_MAX_BYTES="$MAX_MSG" \
  -e KAFKA_REPLICA_FETCH_MAX_BYTES="$MAX_MSG" \
  -e KAFKA_FETCH_MAX_BYTES="$((MAX_MSG * 2))" \
  -e KAFKA_SOCKET_REQUEST_MAX_BYTES="$((MAX_MSG * 2))" \
  -e KAFKA_NUM_NETWORK_THREADS=4 \
  -e KAFKA_NUM_IO_THREADS=8 \
  -e KAFKA_LOG_DIRS=/var/lib/kafka/data \
  -e KAFKA_HEAP_OPTS="-Xms${KAFKA_HEAP} -Xmx${KAFKA_HEAP}" \
  "$KAFKA_IMAGE" >/dev/null

echo -n "waiting for kafka"
for _ in $(seq 1 90); do
  if podman exec "$NAME" /opt/kafka/bin/kafka-broker-api-versions.sh --bootstrap-server localhost:9092 >/dev/null 2>&1; then
    echo " ok"; break
  fi
  echo -n "."; sleep 1
done
podman exec "$NAME" /opt/kafka/bin/kafka-topics.sh --bootstrap-server localhost:9092 --version 2>/dev/null | sed 's/^/kafka /' || true
podman ps --filter name="^${NAME}\$" --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}\t{{.Image}}"
