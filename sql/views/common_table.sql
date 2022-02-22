
CREATE VIEW common_table AS
SELECT table_name
FROM src_table
JOIN tgt_table USING(table_name)
;
