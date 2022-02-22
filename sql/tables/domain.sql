
CREATE TABLE src_domain
(
    domain_name varchar(63) not null,
    owner varchar(63) not null,
    data_type varchar(128) not null,
    collation varchar(63),
    nullable varchar(5) not null,
    data_default BLOB SUB_TYPE TEXT,
    check_constraint BLOB SUB_TYPE TEXT,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_domain PRIMARY KEY (domain_name)
);

CREATE TABLE tgt_domain
(
    domain_name varchar(63) not null,
    owner varchar(63) not null,
    data_type varchar(128) not null,
    collation varchar(63),
    nullable varchar(5) not null,
    data_default BLOB SUB_TYPE TEXT,
    check_constraint BLOB SUB_TYPE TEXT,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_domain PRIMARY KEY (domain_name)
);
