
CREATE TABLE schema_mapping
(
    uid char(36) not null,
    comparison_set_uid char(36) not null,
    schema1 varchar(63) not null,
    schema2 varchar(63) not null,
    no_order integer not null,
    CONSTRAINT pk_schema_mapping PRIMARY KEY (uid),
    CONSTRAINT schema_mapping_ak1 UNIQUE(comparison_set_uid, schema1),
    CONSTRAINT schema_mapping_ak2 UNIQUE(comparison_set_uid, no_order),
    CONSTRAINT schema_mapping_fk1 FOREIGN KEY (comparison_set_uid) REFERENCES comparison_set(uid) ON DELETE CASCADE
);
