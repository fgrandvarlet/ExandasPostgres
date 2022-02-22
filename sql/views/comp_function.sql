
CREATE VIEW comp_function AS
SELECT
    function_name
,   identity_arguments_hash
,   s.identity_arguments    AS src_identity_arguments
,   s.argument_data_types   AS src_argument_data_types
,   s.result_data_type      AS src_result_data_type
,   s.owner                 AS src_owner
,   s.function_type         AS src_function_type
,   s.volatility            AS src_volatility
,   s.parallel              AS src_parallel
,   s.security              AS src_security
,   s.language              AS src_language
,   s.source_code           AS src_source_code
,   s.definition            AS src_definition
,   s.description           AS src_description
,   s.access_privileges     AS src_access_privileges
,   t.identity_arguments    AS tgt_identity_arguments
,   t.argument_data_types   AS tgt_argument_data_types
,   t.result_data_type      AS tgt_result_data_type
,   t.owner                 AS tgt_owner
,   t.function_type         AS tgt_function_type
,   t.volatility            AS tgt_volatility
,   t.parallel              AS tgt_parallel
,   t.security              AS tgt_security
,   t.language              AS tgt_language
,   t.source_code           AS tgt_source_code
,   t.definition            AS tgt_definition
,   t.description           AS tgt_description
,   t.access_privileges     AS tgt_access_privileges
FROM src_function s
JOIN tgt_function t USING (function_name, identity_arguments_hash)
ORDER BY function_name, src_identity_arguments
;
