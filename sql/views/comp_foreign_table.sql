
CREATE VIEW comp_foreign_table AS
SELECT
    foreign_table_name
,   s.owner                 AS src_owner
,   s.server                AS src_server
,   s.fdw_options           AS src_fdw_options
,   s.persistence           AS src_persistence
,   s.is_partition          AS src_is_partition
,   s.has_subclass          AS src_has_subclass
,   s.row_security          AS src_row_security
,   s.force_row_security    AS src_force_row_security
,   s.description           AS src_description
,   s.access_privileges     AS src_access_privileges
,   t.owner                 AS tgt_owner
,   t.server                AS tgt_server
,   t.fdw_options           AS tgt_fdw_options
,   t.persistence           AS tgt_persistence
,   t.is_partition          AS tgt_is_partition
,   t.has_subclass          AS tgt_has_subclass
,   t.row_security          AS tgt_row_security
,   t.force_row_security    AS tgt_force_row_security
,   t.description           AS tgt_description
,   t.access_privileges     AS tgt_access_privileges
FROM src_foreign_table s
JOIN tgt_foreign_table t USING (foreign_table_name)
ORDER BY foreign_table_name
;
