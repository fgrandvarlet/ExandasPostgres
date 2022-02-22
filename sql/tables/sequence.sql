
CREATE TABLE src_sequence
(
    sequence_name varchar(63) not null,
    owner varchar(63) not null,
    data_type varchar(63) not null,
    start_value bigint not null,
    min_value bigint not null,
    max_value bigint not null,
    increment_by bigint not null,
    cycle varchar(5) not null,
    cache_size bigint not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    owned_by varchar(128),
    CONSTRAINT pk_src_sequence PRIMARY KEY (sequence_name)
);

CREATE TABLE tgt_sequence
(
    sequence_name varchar(63) not null,
    owner varchar(63) not null,
    data_type varchar(63) not null,
    start_value bigint not null,
    min_value bigint not null,
    max_value bigint not null,
    increment_by bigint not null,
    cycle varchar(5) not null,
    cache_size bigint not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    owned_by varchar(128),
    CONSTRAINT pk_tgt_sequence PRIMARY KEY (sequence_name)
);
