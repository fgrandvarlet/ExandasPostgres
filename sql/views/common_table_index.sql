
CREATE VIEW common_table_index AS
SELECT table_name FROM common_table
UNION ALL
SELECT materialized_view_name FROM common_materialized_view
;
