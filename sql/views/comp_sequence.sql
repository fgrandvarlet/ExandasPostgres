
CREATE VIEW comp_sequence AS
SELECT
    sequence_name
,   s.owner             AS src_owner
,   s.data_type         AS src_data_type
,   s.start_value       AS src_start_value
,   s.min_value         AS src_min_value
,   s.max_value         AS src_max_value
,   s.increment_by      AS src_increment_by
,   s.cycle             AS src_cycle
,   s.cache_size        AS src_cache_size
,   s.description       AS src_description
,   s.access_privileges AS src_access_privileges
,   s.owned_by          AS src_owned_by
,   t.owner             AS tgt_owner
,   t.data_type         AS tgt_data_type
,   t.start_value       AS tgt_start_value
,   t.min_value         AS tgt_min_value
,   t.max_value         AS tgt_max_value
,   t.increment_by      AS tgt_increment_by
,   t.cycle             AS tgt_cycle
,   t.cache_size        AS tgt_cache_size
,   t.description       AS tgt_description
,   t.access_privileges AS tgt_access_privileges
,   t.owned_by          AS tgt_owned_by
FROM src_sequence s
JOIN tgt_sequence t USING (sequence_name)
ORDER BY sequence_name
;
