
CREATE VIEW comp_exclusion AS
SELECT
    constraint_name
,   table_name
,   s.deferrable    AS src_deferrable
,   s.deferred      AS src_deferred
,   s.definition    AS src_definition
,   t.deferrable    AS tgt_deferrable
,   t.deferred      AS tgt_deferred
,   t.definition    AS tgt_definition
FROM src_exclusion s
JOIN tgt_exclusion t USING (table_name, constraint_name)
ORDER BY table_name, constraint_name
;
