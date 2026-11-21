# matrix-35: a network load balancer in place of HAProxy (B30)

The Patroni 2+2 lab of matrix-19 (Patroni 4.1.5 quorum ANY 2 of 3, etcd in DC1, DC2 and a witness; 12.9k ev/s; 3
dispatchers; workers commit their own writes; clients through 127.0.0.1:6440). HAProxy set up like a cloud network
load balancer: `HAPROXY_INTER=5s HAPROXY_FALL=2 HAPROXY_RISE=2` (`infra/patroni/haproxy.sh`), and with
`HAPROXY_CUT=0` no `on-marked-down shutdown-sessions` (established sessions to a node that stops being the leader stay
open, the default of many NLBs). Outage = the longest stretch with applied/s under 10% of its 90th percentile
(`scripts/drill-outage.py`).

| drills | failure | checks | sessions cut | correct | new leader | outage |
|---|---|---|---|---|---|---|
| H01–H05 (matrix-19) | leader's DC killed | 1 s × 2 | yes | 5/5 | 17–21 s | 18–22 s |
| N1–N5 | leader's DC killed | 5 s × 2 | no | 5/5 | 20–21 s | 24–27 s |
| C1–C5 | leader's DC killed | 5 s × 2 | yes | 5/5 | 21–23 s | 25–30 s |
| F1–F3 | leader's DC frozen 60 s | 5 s × 2 | no | 3/3 | 24 s | **83–84 s** |
| G1–G3 | leader's DC frozen 60 s | 5 s × 2 | yes | 3/3 | 23–24 s | **27–30 s** |

("Correct" = every oracle check but cross-source blocking, which every outage fails by design.)

**Findings:**
1. **Session cutting is the requirement that matters.** A frozen (or partitioned, or powered-off) leader leaves
   no one to reset its connections: without cutting, 1,487–1,750 worker calls per drill hung on it until the
   workers' 60 s operation timeout, and the production phase stretched from 60 s to 116–118 s. With cutting, no
   call was abandoned. A *killed* process on a live host shows no difference (its kernel resets the sessions), so
   kill drills alone would miss this. Required from the NLB: terminate connections on unhealthy targets (AWS NLB:
   `target_health_state.unhealthy.connection_termination.enabled`).
2. **5 s checks cost ~5 s per failover** (24–30 s against 18–22 s): the new leader takes traffic only after
   interval × rise thresholds. Patroni's own detection (~20 s with TTL 20) dominates either way.
3. Nothing was lost in any drill: Octopus's retries and the synchronous quorum held through all 16.

Without cutting on the load balancer, the client side has to bound the damage: short TCP keepalives
(`keepalives_idle`/`tcp_user_timeout` in libpq, `Keepalive` + `Tcp Keepalive Time` in Npgsql) and operation timeouts.
That was not tried here (B30 note).
