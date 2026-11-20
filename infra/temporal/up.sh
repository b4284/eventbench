#!/usr/bin/env bash
# Temporal server (auto-setup image: frontend, history, matching, worker in one container) persisting to the
# benchmark Postgres (databases "temporal" and "temporal_visibility"). Recreated from scratch on every call so
# each run starts with empty histories. Host networking: frontend on 127.0.0.1:7233.
#   EB_TEMPORAL_SHARDS  history shards (default 512; fixed for the life of the databases)
set -euo pipefail
IMAGE=${EB_TEMPORAL_IMAGE:-docker.io/temporalio/auto-setup:latest}
SHARDS=${EB_TEMPORAL_SHARDS:-512}
podman rm -f eb-temporal >/dev/null 2>&1 || true
for db in temporal temporal_visibility; do
  podman exec eb-postgres psql -U bench -d postgres -qc "DROP DATABASE IF EXISTS $db WITH (FORCE)" >/dev/null
done
mkdir -p ~/.cache/eventbench/temporal
cat > ~/.cache/eventbench/temporal/dynamic.yaml <<'YAML'
limit.blobSize.warn:
  - value: 4194304
limit.blobSize.error:
  - value: 8388608
# Rate limits raised far above the defaults: this is a capacity test, not a multi-tenant cluster.
frontend.rps:
  - value: 200000
frontend.namespaceRPS:
  - value: 200000
frontend.globalNamespaceRPS:
  - value: 200000
frontend.persistenceMaxQPS:
  - value: 200000
history.rps:
  - value: 200000
history.persistenceMaxQPS:
  - value: 200000
matching.rps:
  - value: 200000
matching.persistenceMaxQPS:
  - value: 200000
system.persistenceMaxQPS:
  - value: 200000
matching.numTaskqueueReadPartitions:
  - value: 4
matching.numTaskqueueWritePartitions:
  - value: 4
YAML
podman run -d --name eb-temporal --network host \
  -e DB=postgres12 -e DB_PORT=5432 -e POSTGRES_SEEDS=127.0.0.1 -e POSTGRES_USER=bench -e POSTGRES_PWD=bench \
  -e NUM_HISTORY_SHARDS="$SHARDS" -e SQL_MAX_CONNS=50 -e BIND_ON_IP=127.0.0.1 -e TEMPORAL_ADDRESS=127.0.0.1:7233 \
  -e DYNAMIC_CONFIG_FILE_PATH=/etc/temporal/config/dynamicconfig/eb.yaml \
  -v ~/.cache/eventbench/temporal/dynamic.yaml:/etc/temporal/config/dynamicconfig/eb.yaml:ro,Z \
  "$IMAGE" >/dev/null
for _ in $(seq 1 120); do
  podman exec eb-temporal temporal operator cluster health --address 127.0.0.1:7233 2>/dev/null | grep -q SERVING && \
  podman exec eb-temporal temporal operator namespace describe default --address 127.0.0.1:7233 >/dev/null 2>&1 && { echo "temporal ready"; exit 0; }
  sleep 1
done
echo "temporal did not become ready" >&2; podman logs --tail 30 eb-temporal >&2; exit 1
