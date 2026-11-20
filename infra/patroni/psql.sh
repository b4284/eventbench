#!/usr/bin/env bash
# psql against the current Patroni leader (bench database), run inside whichever Patroni container is up.
# Extra arguments go to psql (e.g. -At -c "select 1").
for c in eb-pat1 eb-pat2; do
  if [[ "$(podman inspect -f '{{.State.Status}}' $c 2>/dev/null)" == running ]]; then
    exec podman exec -i -e PGPASSWORD=bench $c psql \
      "host=127.0.0.1,127.0.0.1 port=5441,5442 user=bench dbname=bench target_session_attrs=read-write connect_timeout=3" "$@"
  fi
done
echo "no Patroni node running" >&2; exit 1
