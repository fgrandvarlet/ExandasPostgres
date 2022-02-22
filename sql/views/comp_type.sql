
CREATE VIEW comp_type AS
SELECT
    type_name
,   s.format_type_name  AS src_format_type_name
,   s.owner             AS src_owner
,   s.type_type         AS src_type_type
,   s.type_size         AS src_type_size
,   s.elements          AS src_elements
,   s.description       AS src_description
,   s.access_privileges AS src_access_privileges
,   t.format_type_name  AS tgt_format_type_name
,   t.owner             AS tgt_owner
,   t.type_type         AS tgt_type_type
,   t.type_size         AS tgt_type_size
,   t.elements          AS tgt_elements
,   t.description       AS tgt_description
,   t.access_privileges AS tgt_access_privileges
FROM src_type s
JOIN tgt_type t USING (type_name)
ORDER BY type_name
;
