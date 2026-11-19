#!/usr/bin/env bash
# Failover drill: kill one NATS server (default 1). R3 streams keep a quorum with 2 of 3.
podman kill "eb-nats-${1:-1}" >/dev/null && echo "killed eb-nats-${1:-1}"
