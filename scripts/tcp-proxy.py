#!/usr/bin/env python3
"""Plain TCP forwarder for drills: `tcp-proxy.py LISTEN_PORT:TARGET_PORT ...` (all on 127.0.0.1).
Freeze it with SIGSTOP and resume with SIGCONT: the connections through it hang (no reset), like a network
partition between one client and the database."""
import asyncio, sys

async def pipe(r, w):
    try:
        while data := await r.read(1 << 16):
            w.write(data)
            await w.drain()
    except (ConnectionError, asyncio.CancelledError):
        pass
    finally:
        w.close()

def handler(target):
    async def handle(r, w):
        try:
            tr, tw = await asyncio.open_connection("127.0.0.1", target)
        except OSError:
            w.close()
            return
        await asyncio.gather(pipe(r, tw), pipe(tr, w))
    return handle

async def main():
    servers = []
    for spec in sys.argv[1:]:
        lp, tp = (int(x) for x in spec.split(":"))
        servers.append(await asyncio.start_server(handler(tp), "127.0.0.1", lp))
    await asyncio.gather(*(s.serve_forever() for s in servers))

asyncio.run(main())
