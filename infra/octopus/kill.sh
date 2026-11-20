#!/usr/bin/env bash
# Crash one dispatcher (SIGKILL, like a lost pod): infra/octopus/kill.sh [index=1]
STATE=~/.cache/eventbench/octopus
pid=$(sed -n "${1:-1}p" "$STATE/pids")
kill -KILL "$pid" && echo "killed dispatcher d${1:-1} (pid $pid)"
