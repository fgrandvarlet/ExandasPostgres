
CREATE TABLE src_foreign_table
(
    foreign_table_name varchar(63) not null,
    owner varchar(63) not null,
    server varchar(63) not null,
    fdw_options BLOB SUB_TYPE TEXT,
    persistence varchar(9) not null,
    is_partition varchar(5) not null,
    has_subclass varchar(5) not null,
    row_security varchar(5) not null,
    force_row_security varchar(5) not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_foreign_table PRIMARY KEY (foreign_table_name)
);

CREATE TABLE tgt_foreign_table
(
    foreign_table_name varchar(63) not null,
    owner varchar(63) not null,
    server varchar(63) not null,
    fdw_options BLOB SUB_TYPE TEXT,
    persistence varchar(9) not null,
    is_partition varchar(5) not null,
    has_subclass varchar(5) not null,
    row_security varchar(5) not null,
    force_row_security varchar(5) not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_foreign_table PRIMARY KEY (foreign_table_name)
);
