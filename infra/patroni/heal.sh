#!/usr/bin/env bash
# After a drill: start or unfreeze any Patroni node, then wait for a leader and a streaming synchronous standby.
for c in eb-etcd eb-pat1 eb-pat2; do
  case "$(podman inspect -f '{{.State.Status}}' $c 2>/dev/null)" in
    paused) podman unpause $c >/dev/null ;;
    exited|stopped|created) podman start $c >/dev/null ;;
  esac
done
for _ in $(seq 1 120); do
  c=$(curl -s --max-time 2 127.0.0.1:8008/cluster 2>/dev/null || curl -s --max-time 2 127.0.0.1:8009/cluster 2>/dev/null || true)
  if echo "$c" | grep -q '"role": *"leader"' && echo "$c" | grep -q '"role": *"sync_standby"' && echo "$c" | grep -q '"state": *"streaming"'; then
    echo "$c" | python3 -c 'import json,sys; print(" ".join(m["name"] + "=" + m["role"] for m in json.load(sys.stdin)["members"]))'; exit 0
  fi
  sleep 2
done
echo "cluster not healthy" >&2; exit 1
