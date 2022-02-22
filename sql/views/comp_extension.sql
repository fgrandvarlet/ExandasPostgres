
CREATE VIEW comp_extension AS
SELECT
    extension_name
,   s.owner         AS src_owner
,   s.version       AS src_version
,   s.description   AS src_description
,   t.owner         AS tgt_owner
,   t.version       AS tgt_version
,   t.description   AS tgt_description
FROM src_extension s
JOIN tgt_extension t USING (extension_name)
ORDER BY extension_name
;
