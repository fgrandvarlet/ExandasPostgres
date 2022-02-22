
CREATE VIEW common_materialized_view AS
SELECT materialized_view_name
FROM src_materialized_view
JOIN tgt_materialized_view USING(materialized_view_name)
;
