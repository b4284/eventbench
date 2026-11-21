#!/usr/bin/env bash
# After a drill: start or unfreeze every etcd member and Patroni node, then wait for a leader and all standbys
# streaming.
STATE=~/.cache/eventbench/patroni
for c in $(podman ps -a --format '{{.Names}}' | grep -E '^eb-(etcd|pat[0-9]|dc[12][ab])'); do
  case "$(podman inspect -f '{{.State.Status}}' $c 2>/dev/null)" in
    paused) podman unpause $c >/dev/null ;;
    exited|stopped|created) podman start $c >/dev/null ;;
  esac
done
want=$(( $(wc -l < "$STATE/nodes") - 1 ))
for _ in $(seq 1 180); do
  s=$("$(dirname "$0")/status.sh" 2>/dev/null) && echo "$s" | awk -v w="$want" '$2=="leader"{l=1} $2!="leader" && $3=="streaming"{n++} END{exit !(l && n>=w)}' && {
    echo "$s" | awk '{printf "%s=%s ", $1, $2} END{print ""}'; exit 0; }
  sleep 2
done
echo "cluster not healthy" >&2; "$(dirname "$0")/status.sh" >&2; exit 1
