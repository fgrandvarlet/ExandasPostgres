
CREATE VIEW common_view AS
SELECT view_name
FROM src_view
JOIN tgt_view USING(view_name)
;
