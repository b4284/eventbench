#!/usr/bin/env bash
# psql into the benchmark database.  ./infra/psql.sh            -> direct Postgres
#                                     ./infra/psql.sh bouncer    -> PgBouncer admin console (SHOW POOLS; SHOW STATS;)
if [[ "${1:-}" == "bouncer" ]]; then shift
  exec podman exec -it eb-postgres psql "host=127.0.0.1 port=6432 dbname=pgbouncer user=bench password=bench" "$@"
fi
exec podman exec -it eb-postgres psql -U bench -d bench "$@"
