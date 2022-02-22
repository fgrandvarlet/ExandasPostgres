
CREATE VIEW comp_table AS
SELECT
    table_name
,   s.owner                 AS src_owner
,   s.tablespace_name       AS src_tablespace_name
,   s.persistence           AS src_persistence
,   s.access_method         AS src_access_method
,   s.is_partitioned        AS src_is_partitioned
,   s.is_partition          AS src_is_partition
,   s.has_subclass          AS src_has_subclass
,   s.row_security          AS src_row_security
,   s.force_row_security    AS src_force_row_security
,   s.description           AS src_description
,   s.access_privileges     AS src_access_privileges
,   s.options               AS src_options
,   s.partition_key         AS src_partition_key
,   s.partition_bound       AS src_partition_bound
,   t.owner                 AS tgt_owner
,   t.tablespace_name       AS tgt_tablespace_name
,   t.persistence           AS tgt_persistence
,   t.access_method         AS tgt_access_method
,   t.is_partitioned        AS tgt_is_partitioned
,   t.is_partition          AS tgt_is_partition
,   t.has_subclass          AS tgt_has_subclass
,   t.row_security          AS tgt_row_security
,   t.force_row_security    AS tgt_force_row_security
,   t.description           AS tgt_description
,   t.access_privileges     AS tgt_access_privileges
,   t.options               AS tgt_options
,   t.partition_key         AS tgt_partition_key
,   t.partition_bound       AS tgt_partition_bound
FROM src_table s
JOIN tgt_table t USING (table_name)
ORDER BY table_name
;
