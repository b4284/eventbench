#!/usr/bin/env bash
# Sets the replica set's writeConcernMajorityJournalDefault on eb-mongo.
#   true  (MongoDB default): w:majority waits for the journal fsync -> crash-durable acks
#   false: w:majority acks once applied (no journal wait) -> best effort; pair with Store:Durability=BestEffort
set -euo pipefail
v=${1:?true|false}
podman exec eb-mongo mongosh --quiet --eval "
  const c = rs.conf(); c.writeConcernMajorityJournalDefault = $v; c.version++;
  rs.reconfig(c);
  print('writeConcernMajorityJournalDefault = ' + rs.conf().writeConcernMajorityJournalDefault);"
