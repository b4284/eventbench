#!/usr/bin/env bash
# Failover drill: kill one broker (default 1). Leaders move to the other two; min.insync.replicas=2 still holds.
podman kill "eb-kafka-${1:-1}" >/dev/null && echo "killed eb-kafka-${1:-1}"
