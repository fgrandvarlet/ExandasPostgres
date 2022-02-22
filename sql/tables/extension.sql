
CREATE TABLE src_extension
(
    extension_name varchar(63) not null,
    owner varchar(63) not null,
    version BLOB SUB_TYPE TEXT not null,
    description BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_extension PRIMARY KEY (extension_name)
);

CREATE TABLE tgt_extension
(
    extension_name varchar(63) not null,
    owner varchar(63) not null,
    version BLOB SUB_TYPE TEXT not null,
    description BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_extension PRIMARY KEY (extension_name)
);
