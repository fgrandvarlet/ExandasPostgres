
CREATE VIEW common_foreign_table AS
SELECT foreign_table_name
FROM src_foreign_table
JOIN tgt_foreign_table USING(foreign_table_name)
;
