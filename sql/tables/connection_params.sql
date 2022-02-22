
CREATE TABLE connection_params
(
    uid char(36) not null,
    name varchar(64) not null,
    username varchar(63) not null,
    password varchar(1024) not null,
    host varchar(255) not null,
    port integer not null,
    database varchar(63) not null,
    CONSTRAINT pk_connection_params PRIMARY KEY(uid),
    CONSTRAINT connection_params_ak1 UNIQUE(name)
);
