
CREATE VIEW common_index AS
SELECT index_name
FROM src_index
JOIN tgt_index USING(index_name)
;
