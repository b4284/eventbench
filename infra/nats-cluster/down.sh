#!/usr/bin/env bash
for i in 1 2 3; do podman rm -f eb-nats-$i >/dev/null 2>&1 || true; done; echo "nats cluster removed"
