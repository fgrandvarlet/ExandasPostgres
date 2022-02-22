
CREATE TABLE src_foreign_key
(
    constraint_name varchar(63) not null,
    table_name varchar(63) not null,
    deferrable varchar(5) not null,
    deferred varchar(5) not null,
    validated varchar(5) not null,
    index_name varchar(63),
    referenced_table_name varchar(63),
    update_action varchar(11) not null,
    delete_action varchar(11) not null,
    match_type varchar(7) not null,
    definition BLOB SUB_TYPE TEXT not null,
    CONSTRAINT pk_src_foreign_key PRIMARY KEY (table_name, constraint_name)
);

CREATE TABLE tgt_foreign_key
(
    constraint_name varchar(63) not null,
    table_name varchar(63) not null,
    deferrable varchar(5) not null,
    deferred varchar(5) not null,
    validated varchar(5) not null,
    index_name varchar(63),
    referenced_table_name varchar(63),
    update_action varchar(11) not null,
    delete_action varchar(11) not null,
    match_type varchar(7) not null,
    definition BLOB SUB_TYPE TEXT not null,
    CONSTRAINT pk_tgt_foreign_key PRIMARY KEY (table_name, constraint_name)
);
