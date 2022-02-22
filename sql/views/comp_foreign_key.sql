
CREATE VIEW comp_foreign_key AS
SELECT
    constraint_name
,   table_name
,   s.deferrable            AS src_deferrable
,   s.deferred              AS src_deferred
,   s.validated             AS src_validated
,   s.index_name            AS src_index_name
,   s.referenced_table_name AS src_referenced_table_name
,   s.update_action         AS src_update_action
,   s.delete_action         AS src_delete_action
,   s.match_type            AS src_match_type
,   s.definition            AS src_definition
,   t.deferrable            AS tgt_deferrable
,   t.deferred              AS tgt_deferred
,   t.validated             AS tgt_validated
,   t.index_name            AS tgt_index_name
,   t.referenced_table_name AS tgt_referenced_table_name
,   t.update_action         AS tgt_update_action
,   t.delete_action         AS tgt_delete_action
,   t.match_type            AS tgt_match_type
,   t.definition            AS tgt_definition
FROM src_foreign_key s
JOIN tgt_foreign_key t USING (table_name, constraint_name)
ORDER BY table_name, constraint_name
;
