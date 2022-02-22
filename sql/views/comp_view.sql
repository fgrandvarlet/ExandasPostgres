
CREATE VIEW comp_view AS
SELECT
    view_name
,   s.owner                 AS src_owner
,   s.persistence           AS src_persistence
,   s.definition            AS src_definition
,   s.description           AS src_description
,   s.access_privileges     AS src_access_privileges
,   s.options               AS src_options
,   t.owner                 AS tgt_owner
,   t.persistence           AS tgt_persistence
,   t.definition            AS tgt_definition
,   t.description           AS tgt_description
,   t.access_privileges     AS tgt_access_privileges
,   t.options               AS tgt_options
FROM src_view s
JOIN tgt_view t USING (view_name)
ORDER BY view_name
;
