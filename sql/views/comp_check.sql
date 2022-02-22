
CREATE VIEW comp_check AS
SELECT
    constraint_name
,   table_name
,   s.validated     AS src_validated
,   s.definition    AS src_definition
,   t.validated     AS tgt_validated
,   t.definition    AS tgt_definition
FROM src_check s
JOIN tgt_check t USING (table_name, constraint_name)
ORDER BY table_name, constraint_name
;
