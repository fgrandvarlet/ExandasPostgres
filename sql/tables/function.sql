
CREATE TABLE src_function
(
    function_name varchar(63) not null,
    identity_arguments_hash varchar(32) not null,
    identity_arguments BLOB SUB_TYPE TEXT not null,
    argument_data_types BLOB SUB_TYPE TEXT not null,
    result_data_type varchar(128),
    owner varchar(63) not null,
    function_type varchar(6) not null,
    volatility varchar(9) not null,
    parallel varchar(10) not null,
    security varchar(7) not null,
    language varchar(63) not null,
    source_code BLOB SUB_TYPE TEXT not null,
    definition BLOB SUB_TYPE TEXT,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_function PRIMARY KEY (function_name, identity_arguments_hash)
);

CREATE TABLE tgt_function
(
    function_name varchar(63) not null,
    identity_arguments_hash varchar(32) not null,
    identity_arguments BLOB SUB_TYPE TEXT not null,
    argument_data_types BLOB SUB_TYPE TEXT not null,
    result_data_type varchar(128),
    owner varchar(63) not null,
    function_type varchar(6) not null,
    volatility varchar(9) not null,
    parallel varchar(10) not null,
    security varchar(7) not null,
    language varchar(63) not null,
    source_code BLOB SUB_TYPE TEXT not null,
    definition BLOB SUB_TYPE TEXT,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_function PRIMARY KEY (function_name, identity_arguments_hash)
);
