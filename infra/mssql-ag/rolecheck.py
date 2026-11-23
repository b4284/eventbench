#!/usr/bin/env python3
"""Health endpoint for one AG replica (as Patroni's /primary): GET /primary -> 200 if the replica reports PRIMARY and is
the primary recorded in ~/.cache/eventbench/mssql-ag/primary (written by up.sh and failover.sh).
One long-lived sqlcmd session in the replica's container is asked for the role every 0.5 s (a new `podman exec` per
poll was too slow under load: the listener cut a healthy primary's sessions). An answer older than STALE seconds
counts as down (the replica is dead or hung); the session is restarted if it ends.
usage: rolecheck.py <replica> <http port>"""
import http.server, os, subprocess, sys, threading, time

replica, port = sys.argv[1], int(sys.argv[2])
sqlport = {"ag1": 1433, "ag2": 1436, "ag3": 1438}[replica]
AUTH = os.path.expanduser("~/.cache/eventbench/mssql-ag/primary")
pw = os.environ.get("MSSQL_SA_PASSWORD", "EventBench#2026")
STALE = float(os.environ.get("ROLECHECK_STALE", "5"))
state = {"role": "", "at": 0.0}
Q = b"SET NOCOUNT ON; SELECT 'ROLE=' + role_desc FROM sys.dm_hadr_availability_replica_states WHERE is_local = 1\nGO\n"

def session():
    while True:
        try:
            p = subprocess.Popen(["podman", "exec", "-i", f"eb-{replica}", "/opt/mssql-tools18/bin/sqlcmd", "-C", "-S",
                                  f"127.0.0.1,{sqlport}", "-U", "sa", "-P", pw, "-h", "-1", "-W", "-l", "2"],
                                 stdin=subprocess.PIPE, stdout=subprocess.PIPE, stderr=subprocess.DEVNULL)
            def ask():
                try:
                    while p.poll() is None:
                        p.stdin.write(Q); p.stdin.flush(); time.sleep(0.5)
                except Exception:
                    pass
            threading.Thread(target=ask, daemon=True).start()
            for line in p.stdout:
                line = line.decode(errors="replace").strip()
                if line.startswith("ROLE="):
                    state["role"], state["at"] = line[5:], time.time()
            p.wait()
        except Exception:
            pass
        time.sleep(1)

class H(http.server.BaseHTTPRequestHandler):
    def do_GET(self):
        try:
            chosen = open(AUTH).read().strip()
        except OSError:
            chosen = ""
        # PRIMARY by its own word AND the one the cluster manager (failover.sh) made primary: a returning old primary
        # that still believes it leads is not routed to
        ok = state["role"] == "PRIMARY" and time.time() - state["at"] < STALE and chosen == replica
        self.send_response(200 if ok else 503); self.end_headers(); self.wfile.write(state["role"].encode())
    def log_message(self, *a): pass

threading.Thread(target=session, daemon=True).start()
http.server.ThreadingHTTPServer(("127.0.0.1", port), H).serve_forever()
