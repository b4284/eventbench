#!/usr/bin/env bash
# Prints the current leader's name.
"$(dirname "$0")/status.sh" 2>/dev/null | awk '$2=="leader"{print $1; found=1; exit} END{exit !found}'
