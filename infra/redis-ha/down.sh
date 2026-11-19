#!/usr/bin/env bash
for n in eb-redis-6380 eb-redis-6381 eb-redis-6382 eb-sentinel-26380 eb-sentinel-26381 eb-sentinel-26382; do
  podman rm -f "$n" >/dev/null 2>&1 || true
done
echo "redis-ha removed"
