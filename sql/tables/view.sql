
CREATE TABLE src_view
(
    view_name varchar(63) not null,
    owner varchar(63) not null,
    persistence varchar(9) not null,
    definition BLOB SUB_TYPE TEXT not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_view PRIMARY KEY (view_name)
);

CREATE TABLE tgt_view
(
    view_name varchar(63) not null,
    owner varchar(63) not null,
    persistence varchar(9) not null,
    definition BLOB SUB_TYPE TEXT not null,
    description BLOB SUB_TYPE TEXT,
    access_privileges BLOB SUB_TYPE TEXT,
    options BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_view PRIMARY KEY (view_name)
);
