
CREATE TABLE src_trigger
(
    table_name varchar(63) not null,
    trigger_name varchar(63) not null,
    definition BLOB SUB_TYPE TEXT not null,
    enabled varchar(5) not null,
    CONSTRAINT pk_src_trigger PRIMARY KEY (table_name, trigger_name)
);

CREATE TABLE tgt_trigger
(
    table_name varchar(63) not null,
    trigger_name varchar(63) not null,
    definition BLOB SUB_TYPE TEXT not null,
    enabled varchar(5) not null,
    CONSTRAINT pk_tgt_trigger PRIMARY KEY (table_name, trigger_name)
);
