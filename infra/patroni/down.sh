#!/usr/bin/env bash
# Remove every Patroni node and etcd member, with their data.
for c in $(podman ps -a --format '{{.Names}}' | grep -E '^eb-(etcd|pat[0-9]|dc[12][ab])'); do podman rm -f $c >/dev/null; done
for v in $(podman volume ls --format '{{.Name}}' | grep -E '^eb-(pat[0-9]|dc[12][ab])-data$'); do podman volume rm -f $v >/dev/null; done
echo "patroni cluster removed"
