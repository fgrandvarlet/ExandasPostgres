
CREATE TABLE src_table
(
    table_name varchar(63) not null,
    owner varchar(63) not null,
    tablespace_name varchar(63),
    persistence varchar(9) not null,
    access_method varchar(63),
    is_partitioned varchar(5) not null,
    is_partition varchar(5) not null,
    has_subclass varchar(5) not null,
    row_security varchar(5) not null,
    force_row_security varchar(5) not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    partition_key BLOB SUB_TYPE TEXT,
    partition_bound BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_table PRIMARY KEY (table_name)
);

CREATE TABLE tgt_table
(
    table_name varchar(63) not null,
    owner varchar(63) not null,
    tablespace_name varchar(63),
    persistence varchar(9) not null,
    access_method varchar(63),
    is_partitioned varchar(5) not null,
    is_partition varchar(5) not null,
    has_subclass varchar(5) not null,
    row_security varchar(5) not null,
    force_row_security varchar(5) not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    partition_key BLOB SUB_TYPE TEXT,
    partition_bound BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_table PRIMARY KEY (table_name)
);
