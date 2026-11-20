-- Octopus hooks for the EventBench workload. PL/pgSQL, so they can be created before the bench tables exist
-- (each run recreates eb_*). Installed by infra/octopus/up.sh.

-- Apply hook: called by Octopus's commit transaction with the events that passed Octopus's cursor check.
-- The effect is the worker's state change as JSON (see OctopusStore.Effect); eb_commit applies it to the bench
-- tables with its own checks. Anything but "applied" raises, which rolls back the whole commit (Octopus then
-- retries the batch event by event, so one bad event cannot hold back its neighbours).
CREATE OR REPLACE FUNCTION public.eb_octopus_apply(p_src text[], p_seq bigint[], p_eff bytea[])
RETURNS void LANGUAGE plpgsql AS $$
DECLARE
    i  int;
    e  jsonb;
    rc smallint;
BEGIN
    FOR i IN 1 .. coalesce(array_length(p_src, 1), 0) LOOP
        e := CASE WHEN length(p_eff[i]) = 0 THEN '{"k":0}'::jsonb ELSE convert_from(p_eff[i], 'UTF8')::jsonb END;
        rc := eb_commit(p_src[i]::int, p_seq[i], coalesce((e->>'k')::smallint, 0::smallint), coalesce((e->>'r')::bigint, 0),
                        coalesce((e->>'pv')::int, 0), coalesce((e->>'v')::int, 0), coalesce((e->>'h')::bigint, 0),
                        coalesce((e->>'s')::smallint, 1::smallint), decode(e->>'c', 'base64'),
                        (e->>'us')::int, (e->>'uc')::bigint, decode(e->>'ud', 'base64'), NULL, NULL, false);
        IF rc <> 0 THEN
            RAISE EXCEPTION 'eb_commit returned % for source % seq %', rc, p_src[i], p_seq[i];
        END IF;
    END LOOP;
END $$;

-- Load hook: the context each worker needs, preloaded by the dispatcher with the event.
CREATE OR REPLACE FUNCTION public.eb_octopus_load(p_src text[], p_seq bigint[], p_body bytea[])
RETURNS TABLE(i int, context bytea) LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY
    SELECT k.o::int,
           convert_to(jsonb_build_object('l', s.last_seq, 'v', r.version, 'h', r.hash, 's', r.status,
                                         'c', encode(r.ctx, 'base64'))::text, 'UTF8')
      FROM unnest(p_src, p_body) WITH ORDINALITY AS k(src, body, o)
      JOIN eb_source s ON s.source_id = k.src::int
      LEFT JOIN eb_record r ON r.source_id = s.source_id
                           AND r.record_id = (convert_from(k.body, 'UTF8')::jsonb ->> 'r')::bigint;
END $$;
