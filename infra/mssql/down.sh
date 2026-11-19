#!/usr/bin/env bash
set -euo pipefail
podman rm -f "${MSSQL_CONTAINER:-eb-mssql}" >/dev/null 2>&1 && echo "removed eb-mssql" || echo "eb-mssql not running"
