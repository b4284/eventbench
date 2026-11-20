#!/usr/bin/env bash
# Stop every dispatcher started by up.sh (SIGTERM: drain and hand over; SIGKILL after 15 s).
STATE=~/.cache/eventbench/octopus
[[ -f $STATE/pids ]] || exit 0
pids=$(cat "$STATE/pids")
kill -TERM $pids 2>/dev/null || true
for _ in $(seq 1 30); do kill -0 $pids 2>/dev/null || break; sleep 0.5; done
kill -KILL $pids 2>/dev/null || true
: > "$STATE/pids"
