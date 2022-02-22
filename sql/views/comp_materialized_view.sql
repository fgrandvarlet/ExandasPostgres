
CREATE VIEW comp_materialized_view AS
SELECT
    materialized_view_name
,   s.owner                 AS src_owner
,   s.tablespace_name       AS src_tablespace_name
,   s.persistence           AS src_persistence
,   s.access_method         AS src_access_method
,   s.definition            AS src_definition
,   s.description           AS src_description
,   s.access_privileges     AS src_access_privileges
,   s.options               AS src_options
,   t.owner                 AS tgt_owner
,   t.tablespace_name       AS tgt_tablespace_name
,   t.persistence           AS tgt_persistence
,   t.access_method         AS tgt_access_method
,   t.definition            AS tgt_definition
,   t.description           AS tgt_description
,   t.access_privileges     AS tgt_access_privileges
,   t.options               AS tgt_options
FROM src_materialized_view s
JOIN tgt_materialized_view t USING (materialized_view_name)
ORDER BY materialized_view_name
;
