
CREATE TABLE src_check
(
    constraint_name varchar(63) not null,
    table_name varchar(63) not null,
    validated varchar(5) not null,
    definition BLOB SUB_TYPE TEXT not null,
    CONSTRAINT pk_src_check PRIMARY KEY (table_name, constraint_name)
);

CREATE TABLE tgt_check
(
    constraint_name varchar(63) not null,
    table_name varchar(63) not null,
    validated varchar(5) not null,
    definition BLOB SUB_TYPE TEXT not null,
    CONSTRAINT pk_tgt_check PRIMARY KEY (table_name, constraint_name)
);
