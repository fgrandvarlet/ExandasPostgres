
CREATE TABLE src_type
(
    type_name varchar(63) not null,
    format_type_name varchar(128) not null,
    owner varchar(63) not null,
    type_type varchar(11),
    type_size varchar(32) not null,
    elements BLOB SUB_TYPE TEXT,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_type PRIMARY KEY (type_name)
);

CREATE TABLE tgt_type
(
    type_name varchar(63) not null,
    format_type_name varchar(128) not null,
    owner varchar(63) not null,
    type_type varchar(11),
    type_size varchar(32) not null,
    elements BLOB SUB_TYPE TEXT,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_type PRIMARY KEY (type_name)
);
