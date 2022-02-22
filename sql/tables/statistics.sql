
CREATE TABLE src_statistics
(
    statistics_name varchar(63) not null,
    owner varchar(63) not null,
    definition BLOB SUB_TYPE TEXT not null,
    ndistinct varchar(7),
    dependencies varchar(7),
    mcv varchar(7),
    statistics_target integer,
    description BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_statistics PRIMARY KEY (statistics_name)
);

CREATE TABLE tgt_statistics
(
    statistics_name varchar(63) not null,
    owner varchar(63) not null,
    definition BLOB SUB_TYPE TEXT not null,
    ndistinct varchar(7),
    dependencies varchar(7),
    mcv varchar(7),
    statistics_target integer,
    description BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_statistics PRIMARY KEY (statistics_name)
);
