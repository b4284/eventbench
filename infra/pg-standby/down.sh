#!/usr/bin/env bash
podman rm -f eb-pg-standby >/dev/null 2>&1 || true; echo "standby removed"
