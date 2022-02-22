
CREATE VIEW comp_domain AS
SELECT
    domain_name
,   s.owner             AS src_owner
,   s.data_type         AS src_data_type
,   s.collation         AS src_collation
,   s.nullable          AS src_nullable
,   s.data_default      AS src_data_default
,   s.check_constraint  AS src_check_constraint
,   s.description       AS src_description
,   s.access_privileges AS src_access_privileges
,   t.owner             AS tgt_owner
,   t.data_type         AS tgt_data_type
,   t.collation         AS tgt_collation
,   t.nullable          AS tgt_nullable
,   t.data_default      AS tgt_data_default
,   t.check_constraint  AS tgt_check_constraint
,   t.description       AS tgt_description
,   t.access_privileges AS tgt_access_privileges
FROM src_domain s
JOIN tgt_domain t USING (domain_name)
ORDER BY domain_name
;
