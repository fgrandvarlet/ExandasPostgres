
CREATE TABLE src_unique
(
    constraint_name varchar(63) not null,
    table_name varchar(63) not null,
    deferrable varchar(5) not null,
    deferred varchar(5) not null,
    definition BLOB SUB_TYPE TEXT not null,
    CONSTRAINT pk_src_unique PRIMARY KEY (constraint_name)
);

CREATE TABLE tgt_unique
(
    constraint_name varchar(63) not null,
    table_name varchar(63) not null,
    deferrable varchar(5) not null,
    deferred varchar(5) not null,
    definition BLOB SUB_TYPE TEXT not null,
    CONSTRAINT pk_tgt_unique PRIMARY KEY (constraint_name)
);
