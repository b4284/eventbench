#!/usr/bin/env bash
# psql against the leader through the laptop's HAProxy, run inside a DC1 container (or any eb-patroni image).
c=geo-dc1a; [[ "$(podman inspect -f '{{.State.Status}}' $c 2>/dev/null)" == running ]] || c=geo-dc1b
[[ "$(podman inspect -f '{{.State.Status}}' $c 2>/dev/null)" == running ]] && \
  exec podman exec -i -e PGPASSWORD=bench $c psql "host=127.0.0.1 port=6440 user=bench dbname=bench connect_timeout=3" "$@"
exec podman run --rm -i --network host -e PGPASSWORD=bench --entrypoint psql localhost/eb-patroni:latest "host=127.0.0.1 port=6440 user=bench dbname=bench connect_timeout=3" "$@"
