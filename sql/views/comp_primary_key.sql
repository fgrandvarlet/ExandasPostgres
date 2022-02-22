
CREATE VIEW comp_primary_key AS
SELECT
    constraint_name
,   table_name
,   s.deferrable    AS src_deferrable
,   s.deferred      AS src_deferred
,   s.definition    AS src_definition
,   t.deferrable    AS tgt_deferrable
,   t.deferred      AS tgt_deferred
,   t.definition    AS tgt_definition
FROM src_primary_key s
JOIN tgt_primary_key t USING (table_name, constraint_name)
ORDER BY table_name, constraint_name
;
