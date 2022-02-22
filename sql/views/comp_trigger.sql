
CREATE VIEW comp_trigger AS
SELECT
    table_name
,   trigger_name
,   s.definition    AS src_definition
,   s.enabled       AS src_enabled
,   t.definition    AS tgt_definition
,   t.enabled       AS tgt_enabled
FROM src_trigger s
JOIN tgt_trigger t USING (table_name, trigger_name)
ORDER BY table_name, trigger_name
;
