#!/usr/bin/env bash
# Remove the Patroni pair and etcd, with their data.
podman rm -f eb-pat1 eb-pat2 eb-etcd >/dev/null 2>&1 || true
podman volume rm -f eb-pat1-data eb-pat2-data >/dev/null 2>&1 || true
echo "patroni cluster removed"
