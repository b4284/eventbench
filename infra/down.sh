#!/usr/bin/env bash
# Stops and removes the eventbench pod (data is not persisted between runs).
set -euo pipefail
POD=${POD:-eventbench}
podman pod rm -f "$POD" 2>/dev/null && echo "removed pod $POD" || echo "pod $POD not found"
