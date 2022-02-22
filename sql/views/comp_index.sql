
CREATE VIEW comp_index AS
SELECT
    index_name
,   table_name
,   s.owner             AS src_owner
,   s.tablespace_name   AS src_tablespace_name
,   s.persistence       AS src_persistence
,   s.access_method     AS src_access_method
,   s.is_partitioned    AS src_is_partitioned
,   s.is_partition      AS src_is_partition
,   s.has_subclass      AS src_has_subclass
,   s.is_unique         AS src_is_unique
,   s.is_primary        AS src_is_primary
,   s.is_exclusion      AS src_is_exclusion
,   s.immediate         AS src_immediate
,   s.is_clustered      AS src_is_clustered
,   s.is_valid          AS src_is_valid
,   s.definition        AS src_definition
,   s.description       AS src_description
,   s.options           AS src_options
,   t.owner             AS tgt_owner
,   t.tablespace_name   AS tgt_tablespace_name
,   t.persistence       AS tgt_persistence
,   t.access_method     AS tgt_access_method
,   t.is_partitioned    AS tgt_is_partitioned
,   t.is_partition      AS tgt_is_partition
,   t.has_subclass      AS tgt_has_subclass
,   t.is_unique         AS tgt_is_unique
,   t.is_primary        AS tgt_is_primary
,   t.is_exclusion      AS tgt_is_exclusion
,   t.immediate         AS tgt_immediate
,   t.is_clustered      AS tgt_is_clustered
,   t.is_valid          AS tgt_is_valid
,   t.definition        AS tgt_definition
,   t.description       AS tgt_description
,   t.options           AS tgt_options
FROM src_index s
JOIN tgt_index t USING (index_name, table_name)
ORDER BY table_name, index_name
;
