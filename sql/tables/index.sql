
CREATE TABLE src_index
(
    index_name varchar(63) not null,
    table_name varchar(63) not null,
    owner varchar(63) not null,
    tablespace_name varchar(63),
    persistence varchar(9) not null,
    access_method varchar(63),
    is_partitioned varchar(5) not null,
    is_partition varchar(5) not null,
    has_subclass varchar(5) not null,
    is_unique varchar(5) not null,
    is_primary varchar(5) not null,
    is_exclusion varchar(5) not null,
    immediate varchar(5) not null,
    is_clustered varchar(5) not null,
    is_valid varchar(5) not null,
    definition BLOB SUB_TYPE TEXT not null,
    description BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_index PRIMARY KEY (index_name)
);

CREATE TABLE tgt_index
(
    index_name varchar(63) not null,
    table_name varchar(63) not null,
    owner varchar(63) not null,
    tablespace_name varchar(63),
    persistence varchar(9) not null,
    access_method varchar(63),
    is_partitioned varchar(5) not null,
    is_partition varchar(5) not null,
    has_subclass varchar(5) not null,
    is_unique varchar(5) not null,
    is_primary varchar(5) not null,
    is_exclusion varchar(5) not null,
    immediate varchar(5) not null,
    is_clustered varchar(5) not null,
    is_valid varchar(5) not null,
    definition BLOB SUB_TYPE TEXT not null,
    description BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_index PRIMARY KEY (index_name)
);
