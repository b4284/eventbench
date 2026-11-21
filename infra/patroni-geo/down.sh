#!/usr/bin/env bash
# Stop and remove the three-site cluster, with its data (laptop containers, processes and data on the Macs).
for c in geo-dc1a geo-dc1b geo-etcd-dc1 geo-haproxy; do podman rm -f $c >/dev/null 2>&1; done
for v in geo-dc1a-data geo-dc1b-data; do podman volume rm -f $v >/dev/null 2>&1; done
# Patroni stops its Postgres on SIGTERM; pid files, never pkill -f over ssh (it matches the remote shell itself)
ssh -o BatchMode=yes m4mini 'cd ~/octolab; for n in dc2a dc2b etcd-dc2; do [ -f $n.pid ] && kill $(cat $n.pid) 2>/dev/null; done; sleep 8; for n in dc2a dc2b; do [ -f data/$n/postmaster.pid ] && kill -INT $(head -1 data/$n/postmaster.pid) 2>/dev/null; done; sleep 2; rm -rf data/dc2a data/dc2b data/etcd-dc2 dc2a.pid dc2b.pid etcd-dc2.pid' 2>/dev/null
ssh -o BatchMode=yes m1mini 'cd ~/octolab; [ -f etcd-w.pid ] && kill $(cat etcd-w.pid) 2>/dev/null; sleep 1; rm -rf data/etcd-w etcd-w.pid' 2>/dev/null
echo "geo cluster removed"
