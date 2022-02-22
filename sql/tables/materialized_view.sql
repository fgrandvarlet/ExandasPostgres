
CREATE TABLE src_materialized_view
(
    materialized_view_name varchar(63) not null,
    owner varchar(63) not null,
    tablespace_name varchar(63),
    persistence varchar(9) not null,
    access_method varchar(63),
    definition BLOB SUB_TYPE TEXT not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_materialized_view PRIMARY KEY (materialized_view_name)
);

CREATE TABLE tgt_materialized_view
(
    materialized_view_name varchar(63) not null,
    owner varchar(63) not null,
    tablespace_name varchar(63),
    persistence varchar(9) not null,
    access_method varchar(63),
    definition BLOB SUB_TYPE TEXT not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_materialized_view PRIMARY KEY (materialized_view_name)
);
