import json, statistics, sys, os
from pathlib import Path
root = Path(sys.argv[1]); cores = os.cpu_count()
def lat(j,m,p):
    for l in j["latencyMs"]:
        if l["metric"]==m: return l[p]
def res(j,k): v=j["resourcesSteadyState"].get(k); return v["mean"] if v else None
rows=[]
for d in sorted(p for p in root.iterdir() if p.is_dir()):
    runs=[json.loads(f.read_text()) for f in sorted(d.glob("*/summary.json"))]
    def med(fn):
        v=[x for x in (fn(r) for r in runs) if x is not None]
        return statistics.median(v) if v else None
    ctr=lambda name: med(lambda j: res(j,f"ctr_eb_{name}_cpu_pct"))
    rows.append(dict(
        name=d.name, n=len(runs), v="".join("P" if r["verdict"]=="PASS" else "F" for r in runs),
        offered=med(lambda j:j["topology"]["offeredRatePerSec"]),
        produced=med(lambda j:j["throughput"]["steadyProducedPerSec"]),
        applied=med(lambda j:j["throughput"]["steadyAppliedPerSec"]),
        p50=med(lambda j:lat(j,"e2e","p50")), p99=med(lambda j:lat(j,"e2e","p99")), p999=med(lambda j:lat(j,"e2e","p99_9")),
        lp50=med(lambda j:lat(j,"e2e_large","p50")), lp99=med(lambda j:lat(j,"e2e_large","p99")),
        drain=med(lambda j:j["phases"]["drainSeconds"]),
        pg=ctr("postgres"), pgb=ctr("pgbouncer"), mongo=ctr("mongo"), kafka=ctr("kafka"), rabbit=ctr("rabbitmq"), nats=ctr("nats"), mssql=ctr("mssql"), redis=ctr("redis"),
        bench=med(lambda j:res(j,"bench_cpu_cores")), host=med(lambda j:res(j,"host_cpu_pct")),
        pgbwait=med(lambda j:res(j,"pgb_cl_waiting")),
    ))
f=lambda v,d=0: "-" if v is None else (f"{v:,.{d}f}")
ms=lambda v: "-" if v is None else (f"{v/1000:.1f}s" if v>=1000 else f"{v:.0f}")
print("| combo | oracle | produced/s | applied/s | e2e p50 | e2e p99 | e2e p99.9 | large p50 | large p99 | drain s | CPU cores: pg / pgb / mongo / kafka / rabbit / nats / mssql / redis / bench | host % | pgb waiting |")
print("|---|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---:|---:|")
for r in rows:
    c=" / ".join(f(x/100 if x is not None else None,1) for x in (r["pg"],r["pgb"],r["mongo"],r["kafka"],r["rabbit"],r["nats"],r["mssql"],r["redis"]))+f" / {f(r['bench'],1)}"
    print(f"| {r['name']} | {r['v']} | {f(r['produced'])} | {f(r['applied'])} | {ms(r['p50'])} | {ms(r['p99'])} | {ms(r['p999'])} | {ms(r['lp50'])} | {ms(r['lp99'])} | {f(r['drain'])} | {c} | {f(r['host'])} | {f(r['pgbwait'])} |")
print(f"\noffered load ≈ {f(rows[0]['offered'])} ev/s; medians of {rows[0]['n']} reps; latencies ms unless 's'; CPU = container cores (cpu_time deltas)")
