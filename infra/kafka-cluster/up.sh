#!/usr/bin/env bash
# Production-style Kafka: 3 KRaft nodes (combined broker+controller), host network.
#   brokers 127.0.0.1:9192,9292,9392   controllers :9193,:9293,:9393
#   default.replication.factor=3, min.insync.replicas=2, unclean leader election off; internal topics RF 3.
#   Producers use acks=all + idempotence (KafkaBroker), so an acked message is on >= 2 brokers.
# Durability: replication instead of fsync (log.flush left to the OS), as recommended for Kafka clusters.
# Tunables: KAFKA_IMAGE, KAFKA_HEAP (per node), KAFKA_MAX_MESSAGE_BYTES
set -euo pipefail
IMAGE=${KAFKA_IMAGE:-docker.io/apache/kafka:4.1.1}
HEAP=${KAFKA_HEAP:-1536M}
MAX_MSG=${KAFKA_MAX_MESSAGE_BYTES:-67108864}
CLUSTER_ID=${KAFKA_CLUSTER_ID:-ebKafkaCluster00000001}
VOTERS="1@127.0.0.1:9193,2@127.0.0.1:9293,3@127.0.0.1:9393"
for i in 1 2 3; do
  name=eb-kafka-$i
  podman container exists "$name" && { echo "$name exists (run infra/kafka-cluster/down.sh first)"; exit 0; }
  podman run -d --name "$name" --network host \
    -e KAFKA_NODE_ID=$i -e CLUSTER_ID="$CLUSTER_ID" \
    -e KAFKA_PROCESS_ROLES=broker,controller \
    -e KAFKA_LISTENERS="PLAINTEXT://127.0.0.1:9${i}92,CONTROLLER://127.0.0.1:9${i}93" \
    -e KAFKA_ADVERTISED_LISTENERS="PLAINTEXT://127.0.0.1:9${i}92" \
    -e KAFKA_CONTROLLER_LISTENER_NAMES=CONTROLLER \
    -e KAFKA_LISTENER_SECURITY_PROTOCOL_MAP=CONTROLLER:PLAINTEXT,PLAINTEXT:PLAINTEXT \
    -e KAFKA_CONTROLLER_QUORUM_VOTERS="$VOTERS" \
    -e KAFKA_INTER_BROKER_LISTENER_NAME=PLAINTEXT \
    -e KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=3 -e KAFKA_OFFSETS_TOPIC_NUM_PARTITIONS=10 \
    -e KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR=3 -e KAFKA_TRANSACTION_STATE_LOG_MIN_ISR=2 \
    -e KAFKA_DEFAULT_REPLICATION_FACTOR=3 -e KAFKA_MIN_INSYNC_REPLICAS=2 \
    -e KAFKA_UNCLEAN_LEADER_ELECTION_ENABLE=false \
    -e KAFKA_GROUP_INITIAL_REBALANCE_DELAY_MS=0 -e KAFKA_AUTO_CREATE_TOPICS_ENABLE=false -e KAFKA_DELETE_TOPIC_ENABLE=true \
    -e KAFKA_MESSAGE_MAX_BYTES="$MAX_MSG" -e KAFKA_REPLICA_FETCH_MAX_BYTES="$MAX_MSG" \
    -e KAFKA_FETCH_MAX_BYTES="$((MAX_MSG * 2))" -e KAFKA_SOCKET_REQUEST_MAX_BYTES="$((MAX_MSG * 2))" \
    -e KAFKA_NUM_NETWORK_THREADS=4 -e KAFKA_NUM_IO_THREADS=8 -e KAFKA_NUM_REPLICA_FETCHERS=4 \
    -e KAFKA_LOG_DIRS=/var/lib/kafka/data \
    -e KAFKA_HEAP_OPTS="-Xms${HEAP} -Xmx${HEAP}" \
    "$IMAGE" >/dev/null
done
echo -n "waiting for the cluster"
for _ in $(seq 1 120); do
  n=$(podman exec eb-kafka-1 /opt/kafka/bin/kafka-metadata-quorum.sh --bootstrap-server 127.0.0.1:9192 describe --status 2>/dev/null | grep -c "CurrentVoters") || true
  [[ "$n" -ge 1 ]] && { echo " ok"; break; }
  echo -n "."; sleep 1
done
podman exec eb-kafka-1 /opt/kafka/bin/kafka-metadata-quorum.sh --bootstrap-server 127.0.0.1:9192 describe --status 2>/dev/null | grep -E "LeaderId|CurrentVoters"
