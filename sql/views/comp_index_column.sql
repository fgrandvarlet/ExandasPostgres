
CREATE VIEW comp_index_column AS
SELECT
    index_name
,   column_name
,   s.owner             AS src_owner
,   s.column_num        AS src_column_num
,   s.data_type         AS src_data_type
,   s.collation         AS src_collation
,   s.nullable          AS src_nullable
,   s.data_default      AS src_data_default
,   s.identity          AS src_identity
,   s.generated         AS src_generated
,   s.storage           AS src_storage
,   s.compression       AS src_compression
,   s.statistics_target AS src_statistics_target
,   s.is_local          AS src_is_local
,   s.inheritance_count AS src_inheritance_count
,   s.description       AS src_description
,   s.access_privileges AS src_access_privileges
,   s.options           AS src_options
,   t.owner             AS tgt_owner
,   t.column_num        AS tgt_column_num
,   t.data_type         AS tgt_data_type
,   t.collation         AS tgt_collation
,   t.nullable          AS tgt_nullable
,   t.data_default      AS tgt_data_default
,   t.identity          AS tgt_identity
,   t.generated         AS tgt_generated
,   t.storage           AS tgt_storage
,   t.compression       AS tgt_compression
,   t.statistics_target AS tgt_statistics_target
,   t.is_local          AS tgt_is_local
,   t.inheritance_count AS tgt_inheritance_count
,   t.description       AS tgt_description
,   t.access_privileges AS tgt_access_privileges
,   t.options           AS tgt_options
FROM src_index_column s
JOIN tgt_index_column t USING (index_name, column_name)
ORDER BY index_name, column_name
;
