#!/usr/bin/env bash
for c in $(podman ps -a --format '{{.Names}}' | grep -E '^eb-b-'); do podman rm -f $c >/dev/null; done
for v in $(podman volume ls --format '{{.Name}}' | grep -E '^eb-b-'); do podman volume rm -f $v >/dev/null; done
for n in eb-b-dc1 eb-b-dc2 eb-b-wan; do podman network rm $n >/dev/null 2>&1 || true; done
echo "bridged cluster removed"
