
CREATE TABLE src_foreign_table_column
(
    foreign_table_name varchar(63) not null,
    column_name varchar(63) not null,
    owner varchar(63) not null,
    column_num smallint not null,
    data_type varchar(128) not null,
    collation varchar(63),
    nullable varchar(5) not null,
    data_default BLOB SUB_TYPE TEXT,
    identity varchar(10),
    generated varchar(6),
    storage varchar(8) not null,
    compression varchar(4),
    statistics_target integer,
    is_local varchar(5) not null,
    inheritance_count integer not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    fdw_options BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_foreign_table_column PRIMARY KEY (foreign_table_name, column_name)
);

CREATE TABLE tgt_foreign_table_column
(
    foreign_table_name varchar(63) not null,
    column_name varchar(63) not null,
    owner varchar(63) not null,
    column_num smallint not null,
    data_type varchar(128) not null,
    collation varchar(63),
    nullable varchar(5) not null,
    data_default BLOB SUB_TYPE TEXT,
    identity varchar(10),
    generated varchar(6),
    storage varchar(8) not null,
    compression varchar(4),
    statistics_target integer,
    is_local varchar(5) not null,
    inheritance_count integer not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    fdw_options BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_foreign_table_column PRIMARY KEY (foreign_table_name, column_name)
);
