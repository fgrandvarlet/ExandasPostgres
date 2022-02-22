
CREATE VIEW comp_statistics AS
SELECT
    statistics_name
,   s.owner             AS src_owner
,   s.definition        AS src_definition
,   s.ndistinct         AS src_ndistinct
,   s.dependencies      AS src_dependencies
,   s.mcv               AS src_mcv
,   s.statistics_target AS src_statistics_target
,   s.description       AS src_description
,   t.owner             AS tgt_owner
,   t.definition        AS tgt_definition
,   t.ndistinct         AS tgt_ndistinct
,   t.dependencies      AS tgt_dependencies
,   t.mcv               AS tgt_mcv
,   t.statistics_target AS tgt_statistics_target
,   t.description       AS tgt_description
FROM src_statistics s
JOIN tgt_statistics t USING (statistics_name)
ORDER BY statistics_name
;
