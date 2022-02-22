using System;
using System.Collections.Generic;
using Npgsql;

using ExandasPostgres.Core;
using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao.PostgreSQL
{
    public class RemoteDaoPostgreSQL : AbstractDaoPostgreSQL, IRemoteDao
    {
        public RemoteDaoPostgreSQL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckConnection()
        {
            bool result = false;
            using (NpgsqlConnection conn = GetNpgsqlConnection())
            {
                try
                {
                    conn.Open();
                    result = true;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Version GetVersion()
        {
            using (NpgsqlConnection conn = GetNpgsqlConnection())
            {
                try
                {
                    conn.Open();
                    return conn.PostgreSqlVersion;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        public bool CheckExistsSchema(string schemaName)
        {
            const string sql = "SELECT 1 FROM pg_catalog.pg_namespace WHERE nspname = @schema";

            using (NpgsqlConnection conn = GetNpgsqlConnection())
            {
                try
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("schema", schemaName);

                    object result = cmd.ExecuteScalar();
                    return result != null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Table> GetTableList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Table>();

            string sql = $@"SELECT
    c.relname AS table_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    ts.spcname AS tablespace_name,
    CASE c.relpersistence WHEN 'p' THEN 'permanent' WHEN 't' THEN 'temporary' WHEN 'u' THEN 'unlogged' END::varchar(9) AS persistence,
    am.amname AS access_method,
    CASE c.relkind WHEN 'p' THEN TRUE ELSE FALSE END::text AS is_partitioned,
    c.relispartition::text AS is_partition,
    c.relhassubclass::text AS has_subclass,
    c.relrowsecurity::text AS row_security,
    c.relforcerowsecurity::text AS force_row_security,
    pg_catalog.obj_description(c.oid, 'pg_class') AS description,
    pg_catalog.array_to_string(c.relacl, ',') AS access_privileges,
    pg_catalog.array_to_string(c.reloptions, ',') AS options,
    pg_catalog.pg_get_partkeydef(c.oid) AS partition_key,
    pg_catalog.pg_get_expr(c.relpartbound, c.oid, TRUE) AS partition_bound
FROM pg_catalog.pg_class c
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
LEFT JOIN pg_catalog.pg_am am ON am.oid = c.relam
LEFT JOIN pg_catalog.pg_tablespace ts ON ts.oid = c.reltablespace
WHERE c.relkind IN ('r','p')
AND n.nspname = @schema
ORDER BY table_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ta = new Table
                    {
                        TableName = (string)dr["table_name"],
                        Owner = (string)dr["owner"],
                        TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"],
                        Persistence = (string)dr["persistence"],
                        AccessMethod = dr["access_method"] is DBNull ? null : (string)dr["access_method"],
                        IsPartitioned = (string)dr["is_partitioned"],
                        IsPartition = (string)dr["is_partition"],
                        HasSubclass = (string)dr["has_subclass"],
                        RowSecurity = (string)dr["row_security"],
                        ForceRowSecurity = (string)dr["force_row_security"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                        Options = dr["options"] is DBNull ? null : (string)dr["options"],
                        PartitionKey = dr["partition_key"] is DBNull ? null : (string)dr["partition_key"],
                        PartitionBound = dr["partition_bound"] is DBNull ? null : (string)dr["partition_bound"],
                    };
                    list.Add(ta);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<TableColumn> GetTableColumnList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<TableColumn>();

            string sql = $@"SELECT
    c.relname AS table_name,
    a.attname AS column_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    a.attnum AS column_num,
    pg_catalog.format_type(a.atttypid, a.atttypmod)::varchar(128) AS data_type,
    (SELECT c.collname FROM pg_catalog.pg_collation c, pg_catalog.pg_type t
     WHERE c.oid = a.attcollation AND t.oid = a.atttypid AND a.attcollation <> t.typcollation) AS collation,
    (NOT a.attnotnull)::text AS nullable,
    (SELECT pg_catalog.pg_get_expr(d.adbin, d.adrelid, true)
     FROM pg_catalog.pg_attrdef d
     WHERE d.adrelid = a.attrelid AND d.adnum = a.attnum AND a.atthasdef) AS data_default,
    CASE a.attidentity WHEN 'a' THEN 'always' WHEN 'd' THEN 'by default' ELSE NULL END::varchar(10) AS identity,
    {(pgVersion.IsGreaterOrEqual(12, 0) ? "CASE a.attgenerated WHEN 's' THEN 'stored' ELSE NULL END::varchar(6)" : "NULL")} AS generated,
    CASE a.attstorage WHEN 'p' THEN 'plain' WHEN 'e' THEN 'external' WHEN 'm' THEN 'main' WHEN 'x' THEN 'extended' END::varchar(8) AS storage,
    {(pgVersion.IsGreaterOrEqual(14, 0) ? "CASE a.attcompression WHEN 'p' THEN 'pglz' WHEN 'l' THEN 'LZ4' ELSE NULL END::varchar(4)" : "NULL")} AS compression,
    CASE WHEN a.attstattarget = -1 THEN NULL ELSE a.attstattarget END AS statistics_target,
    a.attislocal::text AS is_local,
    a.attinhcount AS inheritance_count,
    pg_catalog.col_description(a.attrelid, a.attnum) AS description,
    pg_catalog.array_to_string(a.attacl, ',') AS access_privileges,
    pg_catalog.array_to_string(a.attoptions, ',') AS options
FROM pg_catalog.pg_attribute a
JOIN pg_catalog.pg_class c ON c.oid = a.attrelid
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE c.relkind IN ('r','p') AND a.attnum > 0 AND NOT a.attisdropped
AND n.nspname = @schema
ORDER BY table_name, column_num";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var tc = new TableColumn
                    {
                        RelationName = (string)dr["table_name"],
                        ColumnName = (string)dr["column_name"],
                        Owner = (string)dr["owner"],
                        ColumnNum = (short)dr["column_num"],
                        DataType = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_type"], schema),
                        Collation = dr["collation"] is DBNull ? null : (string)dr["collation"],
                        Nullable = (string)dr["nullable"],
                        DataDefault = dr["data_default"] is DBNull ? null : NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_default"], schema),
                        Identity = dr["identity"] is DBNull ? null : (string)dr["identity"],
                        Generated = dr["generated"] is DBNull ? null : (string)dr["generated"],
                        Storage = (string)dr["storage"],
                        Compression = dr["compression"] is DBNull ? null : (string)dr["compression"],
                        StatisticsTarget = dr["statistics_target"] is DBNull ? null : (int?)dr["statistics_target"],
                        IsLocal = (string)dr["is_local"],
                        InheritanceCount = (int)dr["inheritance_count"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                        Options = dr["options"] is DBNull ? null : (string)dr["options"],
                    };
                    list.Add(tc);
                }
            }

            // renumérotation des colonnes pour gérer les suppressions de colonne
            string currentRelationName = "";
            short num = 0;
            foreach (TableColumn tc in list)
            {
                if (tc.RelationName != currentRelationName)
                {
                    num = 0;
                    currentRelationName = tc.RelationName;
                }
                num++;
                tc.ColumnNum = num;
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<PrimaryKey> GetPrimaryKeyList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<PrimaryKey>();

            string sql = $@"SELECT
    con.conname AS constraint_name,
    c.relname AS table_name,
    con.condeferrable::text AS deferrable,
    con.condeferred::text AS deferred,
    pg_catalog.pg_get_constraintdef(con.oid, true) AS definition
FROM pg_catalog.pg_constraint con
JOIN pg_catalog.pg_namespace n ON n.oid = con.connamespace
JOIN pg_catalog.pg_class c ON c.oid = con.conrelid
WHERE con.contype = 'p'
AND n.nspname = @schema
ORDER BY table_name, constraint_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var pk = new PrimaryKey
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["deferrable"],
                        Deferred = (string)dr["deferred"],
                        Definition = (string)dr["definition"],
                    };
                    list.Add(pk);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Unique> GetUniqueList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Unique>();

            string sql = $@"SELECT
    con.conname AS constraint_name,
    c.relname AS table_name,
    con.condeferrable::text AS deferrable,
    con.condeferred::text AS deferred,
    pg_catalog.pg_get_constraintdef(con.oid, true) AS definition
FROM pg_catalog.pg_constraint con
JOIN pg_catalog.pg_namespace n ON n.oid = con.connamespace
JOIN pg_catalog.pg_class c ON c.oid = con.conrelid
WHERE con.contype = 'u'
AND n.nspname = @schema
ORDER BY table_name, constraint_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var un = new Unique
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["deferrable"],
                        Deferred = (string)dr["deferred"],
                        Definition = (string)dr["definition"],
                    };
                    list.Add(un);
                }
            }
            return list;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<ForeignKey> GetForeignKeyList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<ForeignKey>();

            string sql = $@"SELECT
    con.conname AS constraint_name,
    c.relname AS table_name,
    con.condeferrable::text AS deferrable,
    con.condeferred::text AS deferred,
    con.convalidated::text AS validated,
    ci.relname AS index_name,
    cr.relname AS referenced_table_name,
    CASE con.confupdtype WHEN 'a' THEN 'no action' WHEN 'r' THEN 'restrict' WHEN 'c' THEN 'cascade' WHEN 'n' THEN 'set null' WHEN 'd' THEN 'set default' END::varchar(11) AS update_action,
    CASE con.confdeltype WHEN 'a' THEN 'no action' WHEN 'r' THEN 'restrict' WHEN 'c' THEN 'cascade' WHEN 'n' THEN 'set null' WHEN 'd' THEN 'set default' END::varchar(11) AS delete_action,
    CASE con.confmatchtype WHEN 'f' THEN 'full' WHEN 'p' THEN 'partial' WHEN 's' THEN 'simple' END::varchar(7) AS match_type,
    pg_catalog.pg_get_constraintdef(con.oid, true) AS definition
FROM pg_catalog.pg_constraint con
JOIN pg_catalog.pg_namespace n ON n.oid = con.connamespace
JOIN pg_catalog.pg_class c ON c.oid = con.conrelid
LEFT JOIN pg_catalog.pg_class ci ON ci.oid = con.conindid
LEFT JOIN pg_catalog.pg_class cr ON cr.oid = con.confrelid
WHERE con.contype = 'f'
AND n.nspname = @schema
ORDER BY table_name, constraint_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var fk = new ForeignKey
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["deferrable"],
                        Deferred = (string)dr["deferred"],
                        Validated = (string)dr["validated"],
                        IndexName = dr["index_name"] is DBNull ? null : (string)dr["index_name"],
                        ReferencedTableName = dr["referenced_table_name"] is DBNull ? null : (string)dr["referenced_table_name"],
                        UpdateAction = (string)dr["update_action"],
                        DeleteAction = (string)dr["delete_action"],
                        MatchType = (string)dr["match_type"],
                        Definition = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["definition"], schema),
                    };
                    list.Add(fk);
                }
            }
            return list;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Check> GetCheckList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Check>();

            string sql = $@"SELECT
    con.conname AS constraint_name,
    c.relname AS table_name,
    con.convalidated::text AS validated,
    pg_catalog.pg_get_constraintdef(con.oid, true) AS definition
FROM pg_catalog.pg_constraint con
JOIN pg_catalog.pg_namespace n ON n.oid = con.connamespace
JOIN pg_catalog.pg_class c ON c.oid = con.conrelid
WHERE con.contype = 'c'
AND n.nspname = @schema
ORDER BY table_name, constraint_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ck = new Check
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Validated = (string)dr["validated"],
                        Definition = (string)dr["definition"],
                    };
                    list.Add(ck);
                }
            }
            return list;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Exclusion> GetExclusionList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Exclusion>();

            string sql = $@"SELECT
    con.conname AS constraint_name,
    c.relname AS table_name,
    con.condeferrable::text AS deferrable,
    con.condeferred::text AS deferred,
    pg_catalog.pg_get_constraintdef(con.oid, true) AS definition
FROM pg_catalog.pg_constraint con
JOIN pg_catalog.pg_namespace n ON n.oid = con.connamespace
JOIN pg_catalog.pg_class c ON c.oid = con.conrelid
WHERE con.contype = 'x'
AND n.nspname = @schema
ORDER BY table_name, constraint_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ex = new Exclusion
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["deferrable"],
                        Deferred = (string)dr["deferred"],
                        Definition = (string)dr["definition"],
                    };
                    list.Add(ex);
                }
            }
            return list;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<View> GetViewList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<View>();

            string sql = $@"SELECT
    c.relname AS view_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    CASE c.relpersistence WHEN 'p' THEN 'permanent' WHEN 't' THEN 'temporary' WHEN 'u' THEN 'unlogged' END::varchar(9) AS persistence,
    pg_catalog.pg_get_viewdef(c.oid, true) AS definition,
    pg_catalog.obj_description(c.oid, 'pg_class') AS description,
    pg_catalog.array_to_string(c.relacl, ',') AS access_privileges,
    pg_catalog.array_to_string(c.reloptions, ',') AS options
FROM pg_catalog.pg_class c
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE c.relkind = 'v'
AND n.nspname = @schema
ORDER BY view_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var vi = new View
                    {
                        ViewName = (string)dr["view_name"],
                        Owner = (string)dr["owner"],
                        Persistence = (string)dr["persistence"],
                        Definition = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["definition"], schema),
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                        Options = dr["options"] is DBNull ? null : (string)dr["options"],
                    };
                    list.Add(vi);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<ViewColumn> GetViewColumnList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<ViewColumn>();

            string sql = $@"SELECT
    c.relname AS view_name,
    a.attname AS column_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    a.attnum AS column_num,
    pg_catalog.format_type(a.atttypid, a.atttypmod)::varchar(128) AS data_type,
    (SELECT c.collname FROM pg_catalog.pg_collation c, pg_catalog.pg_type t
     WHERE c.oid = a.attcollation AND t.oid = a.atttypid AND a.attcollation <> t.typcollation) AS collation,
    (NOT a.attnotnull)::text AS nullable,
    (SELECT pg_catalog.pg_get_expr(d.adbin, d.adrelid, true)
     FROM pg_catalog.pg_attrdef d
     WHERE d.adrelid = a.attrelid AND d.adnum = a.attnum AND a.atthasdef) AS data_default,
    CASE a.attidentity WHEN 'a' THEN 'always' WHEN 'd' THEN 'by default' ELSE NULL END::varchar(10) AS identity,
    {(pgVersion.IsGreaterOrEqual(12, 0) ? "CASE a.attgenerated WHEN 's' THEN 'stored' ELSE NULL END::varchar(6)" : "NULL")} AS generated,
    CASE a.attstorage WHEN 'p' THEN 'plain' WHEN 'e' THEN 'external' WHEN 'm' THEN 'main' WHEN 'x' THEN 'extended' END::varchar(8) AS storage,
    {(pgVersion.IsGreaterOrEqual(14, 0) ? "CASE a.attcompression WHEN 'p' THEN 'pglz' WHEN 'l' THEN 'LZ4' ELSE NULL END::varchar(4)" : "NULL")} AS compression,
    CASE WHEN a.attstattarget = -1 THEN NULL ELSE a.attstattarget END AS statistics_target,
    a.attislocal::text AS is_local,
    a.attinhcount AS inheritance_count,
    pg_catalog.col_description(a.attrelid, a.attnum) AS description,
    pg_catalog.array_to_string(a.attacl, ',') AS access_privileges,
    pg_catalog.array_to_string(a.attoptions, ',') AS options
FROM pg_catalog.pg_attribute a
JOIN pg_catalog.pg_class c ON c.oid = a.attrelid
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE c.relkind = 'v'
AND n.nspname = @schema
ORDER BY view_name, column_num";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var vc = new ViewColumn
                    {
                        RelationName = (string)dr["view_name"],
                        ColumnName = (string)dr["column_name"],
                        Owner = (string)dr["owner"],
                        ColumnNum = (short)dr["column_num"],
                        DataType = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_type"], schema),
                        Collation = dr["collation"] is DBNull ? null : (string)dr["collation"],
                        Nullable = (string)dr["nullable"],
                        DataDefault = dr["data_default"] is DBNull ? null : NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_default"], schema),
                        Identity = dr["identity"] is DBNull ? null : (string)dr["identity"],
                        Generated = dr["generated"] is DBNull ? null : (string)dr["generated"],
                        Storage = (string)dr["storage"],
                        Compression = dr["compression"] is DBNull ? null : (string)dr["compression"],
                        StatisticsTarget = dr["statistics_target"] is DBNull ? null : (int?)dr["statistics_target"],
                        IsLocal = (string)dr["is_local"],
                        InheritanceCount = (int)dr["inheritance_count"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                        Options = dr["options"] is DBNull ? null : (string)dr["options"],
                    };
                    list.Add(vc);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<MaterializedView> GetMaterializedViewList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<MaterializedView>();

            string sql = $@"SELECT
    c.relname AS materialized_view_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    ts.spcname AS tablespace_name,
    CASE c.relpersistence WHEN 'p' THEN 'permanent' WHEN 't' THEN 'temporary' WHEN 'u' THEN 'unlogged' END::varchar(9) AS persistence,
    am.amname AS access_method,
    pg_catalog.pg_get_viewdef(c.oid, true) AS definition,
    pg_catalog.obj_description(c.oid, 'pg_class') AS description,
    pg_catalog.array_to_string(c.relacl, ',') AS access_privileges,
    pg_catalog.array_to_string(c.reloptions, ',') AS options
FROM pg_catalog.pg_class c
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
LEFT JOIN pg_catalog.pg_am am ON am.oid = c.relam
LEFT JOIN pg_catalog.pg_tablespace ts ON ts.oid = c.reltablespace
WHERE c.relkind = 'm'
AND n.nspname = @schema
ORDER BY materialized_view_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var mv = new MaterializedView
                    {
                        MaterializedViewName = (string)dr["materialized_view_name"],
                        Owner = (string)dr["owner"],
                        TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"],
                        Persistence = (string)dr["persistence"],
                        AccessMethod = dr["access_method"] is DBNull ? null : (string)dr["access_method"],
                        Definition = (string)dr["definition"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                        Options = dr["options"] is DBNull ? null : (string)dr["options"],
                    };
                    list.Add(mv);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<MaterializedViewColumn> GetMaterializedViewColumnList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<MaterializedViewColumn>();

            string sql = $@"SELECT
    c.relname AS materialized_view_name,
    a.attname AS column_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    a.attnum AS column_num,
    pg_catalog.format_type(a.atttypid, a.atttypmod)::varchar(128) AS data_type,
    (SELECT c.collname FROM pg_catalog.pg_collation c, pg_catalog.pg_type t
     WHERE c.oid = a.attcollation AND t.oid = a.atttypid AND a.attcollation <> t.typcollation) AS collation,
    (NOT a.attnotnull)::text AS nullable,
    (SELECT pg_catalog.pg_get_expr(d.adbin, d.adrelid, true)
     FROM pg_catalog.pg_attrdef d
     WHERE d.adrelid = a.attrelid AND d.adnum = a.attnum AND a.atthasdef) AS data_default,
    CASE a.attidentity WHEN 'a' THEN 'always' WHEN 'd' THEN 'by default' ELSE NULL END::varchar(10) AS identity,
    {(pgVersion.IsGreaterOrEqual(12, 0) ? "CASE a.attgenerated WHEN 's' THEN 'stored' ELSE NULL END::varchar(6)" : "NULL")} AS generated,
    CASE a.attstorage WHEN 'p' THEN 'plain' WHEN 'e' THEN 'external' WHEN 'm' THEN 'main' WHEN 'x' THEN 'extended' END::varchar(8) AS storage,
    {(pgVersion.IsGreaterOrEqual(14, 0) ? "CASE a.attcompression WHEN 'p' THEN 'pglz' WHEN 'l' THEN 'LZ4' ELSE NULL END::varchar(4)" : "NULL")} AS compression,
    CASE WHEN a.attstattarget = -1 THEN NULL ELSE a.attstattarget END AS statistics_target,
    a.attislocal::text AS is_local,
    a.attinhcount AS inheritance_count,
    pg_catalog.col_description(a.attrelid, a.attnum) AS description,
    pg_catalog.array_to_string(a.attacl, ',') AS access_privileges,
    pg_catalog.array_to_string(a.attoptions, ',') AS options
FROM pg_catalog.pg_attribute a
JOIN pg_catalog.pg_class c ON c.oid = a.attrelid
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE c.relkind = 'm' AND a.attnum > 0
AND n.nspname = @schema
ORDER BY materialized_view_name, column_num";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var mvc = new MaterializedViewColumn
                    {
                        RelationName = (string)dr["materialized_view_name"],
                        ColumnName = (string)dr["column_name"],
                        Owner = (string)dr["owner"],
                        ColumnNum = (short)dr["column_num"],
                        DataType = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_type"], schema),
                        Collation = dr["collation"] is DBNull ? null : (string)dr["collation"],
                        Nullable = (string)dr["nullable"],
                        DataDefault = dr["data_default"] is DBNull ? null : NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_default"], schema),
                        Identity = dr["identity"] is DBNull ? null : (string)dr["identity"],
                        Generated = dr["generated"] is DBNull ? null : (string)dr["generated"],
                        Storage = (string)dr["storage"],
                        Compression = dr["compression"] is DBNull ? null : (string)dr["compression"],
                        StatisticsTarget = dr["statistics_target"] is DBNull ? null : (int?)dr["statistics_target"],
                        IsLocal = (string)dr["is_local"],
                        InheritanceCount = (int)dr["inheritance_count"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                        Options = dr["options"] is DBNull ? null : (string)dr["options"],
                    };
                    list.Add(mvc);
                }
            }
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<ForeignTable> GetForeignTableList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<ForeignTable>();

            string sql = $@"SELECT
    c.relname AS foreign_table_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    s.srvname AS server,
    pg_catalog.array_to_string(ARRAY(SELECT pg_catalog.quote_ident(option_name) || ' ' || pg_catalog.quote_literal(option_value)
     FROM pg_catalog.pg_options_to_table(ft.ftoptions)), ',') AS fdw_options,
    CASE c.relpersistence WHEN 'p' THEN 'permanent' WHEN 't' THEN 'temporary' WHEN 'u' THEN 'unlogged' END::varchar(9) AS persistence,
    c.relispartition::text AS is_partition,
    c.relhassubclass::text AS has_subclass,
    c.relrowsecurity::text AS row_security,
    c.relforcerowsecurity::text AS force_row_security,
    pg_catalog.obj_description(c.oid, 'pg_class') AS description,
    pg_catalog.array_to_string(c.relacl, ',') AS access_privileges
FROM pg_catalog.pg_class c
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
JOIN pg_catalog.pg_foreign_table ft ON c.oid = ft.ftrelid
JOIN pg_catalog.pg_foreign_server s ON s.oid = ft.ftserver
WHERE c.relkind = 'f'
AND n.nspname = @schema
ORDER BY foreign_table_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ft = new ForeignTable
                    {
                        ForeignTableName = (string)dr["foreign_table_name"],
                        Owner = (string)dr["owner"],
                        Server = (string)dr["server"],
                        FdwOptions = dr["fdw_options"] is DBNull ? null : (string)dr["fdw_options"],
                        Persistence = (string)dr["persistence"],
                        IsPartition = (string)dr["is_partition"],
                        HasSubclass = (string)dr["has_subclass"],
                        RowSecurity = (string)dr["row_security"],
                        ForceRowSecurity = (string)dr["force_row_security"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                    };
                    list.Add(ft);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<ForeignTableColumn> GetForeignTableColumnList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<ForeignTableColumn>();

            string sql = $@"SELECT
    c.relname AS foreign_table_name,
    a.attname AS column_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    a.attnum AS column_num,
    pg_catalog.format_type(a.atttypid, a.atttypmod)::varchar(128) AS data_type,
    (SELECT c.collname FROM pg_catalog.pg_collation c, pg_catalog.pg_type t
     WHERE c.oid = a.attcollation AND t.oid = a.atttypid AND a.attcollation <> t.typcollation) AS collation,
    (NOT a.attnotnull)::text AS nullable,
    (SELECT pg_catalog.pg_get_expr(d.adbin, d.adrelid, true)
     FROM pg_catalog.pg_attrdef d
     WHERE d.adrelid = a.attrelid AND d.adnum = a.attnum AND a.atthasdef) AS data_default,
    CASE a.attidentity WHEN 'a' THEN 'always' WHEN 'd' THEN 'by default' ELSE NULL END::varchar(10) AS identity,
    {(pgVersion.IsGreaterOrEqual(12, 0) ? "CASE a.attgenerated WHEN 's' THEN 'stored' ELSE NULL END::varchar(6)" : "NULL")} AS generated,
    CASE a.attstorage WHEN 'p' THEN 'plain' WHEN 'e' THEN 'external' WHEN 'm' THEN 'main' WHEN 'x' THEN 'extended' END::varchar(8) AS storage,
    {(pgVersion.IsGreaterOrEqual(14, 0) ? "CASE a.attcompression WHEN 'p' THEN 'pglz' WHEN 'l' THEN 'LZ4' ELSE NULL END::varchar(4)" : "NULL")} AS compression,
    CASE WHEN a.attstattarget = -1 THEN NULL ELSE a.attstattarget END AS statistics_target,
    a.attislocal::text AS is_local,
    a.attinhcount AS inheritance_count,
    pg_catalog.col_description(a.attrelid, a.attnum) AS description,
    pg_catalog.array_to_string(a.attacl, ',') AS access_privileges,
    pg_catalog.array_to_string(a.attoptions, ',') AS options,
    pg_catalog.array_to_string(ARRAY(SELECT pg_catalog.quote_ident(option_name) || ' ' || pg_catalog.quote_literal(option_value)
     FROM pg_catalog.pg_options_to_table(a.attfdwoptions)), ',') AS fdw_options
FROM pg_catalog.pg_attribute a
JOIN pg_catalog.pg_class c ON c.oid = a.attrelid
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE c.relkind = 'f' AND a.attnum > 0 AND NOT a.attisdropped
AND n.nspname = @schema
ORDER BY foreign_table_name, column_num";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ftc = new ForeignTableColumn
                    {
                        RelationName = (string)dr["foreign_table_name"],
                        ColumnName = (string)dr["column_name"],
                        Owner = (string)dr["owner"],
                        ColumnNum = (short)dr["column_num"],
                        DataType = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_type"], schema),
                        Collation = dr["collation"] is DBNull ? null : (string)dr["collation"],
                        Nullable = (string)dr["nullable"],
                        DataDefault = dr["data_default"] is DBNull ? null : NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_default"], schema),
                        Identity = dr["identity"] is DBNull ? null : (string)dr["identity"],
                        Generated = dr["generated"] is DBNull ? null : (string)dr["generated"],
                        Storage = (string)dr["storage"],
                        Compression = dr["compression"] is DBNull ? null : (string)dr["compression"],
                        StatisticsTarget = dr["statistics_target"] is DBNull ? null : (int?)dr["statistics_target"],
                        IsLocal = (string)dr["is_local"],
                        InheritanceCount = (int)dr["inheritance_count"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                        Options = dr["options"] is DBNull ? null : (string)dr["options"],
                        FdwOptions = dr["fdw_options"] is DBNull ? null : (string)dr["fdw_options"],
                    };
                    list.Add(ftc);
                }
            }

            // renumérotation des colonnes pour gérer les suppressions de colonne
            string currentRelationName = "";
            short num = 0;
            foreach (ForeignTableColumn ftc in list)
            {
                if (ftc.RelationName != currentRelationName)
                {
                    num = 0;
                    currentRelationName = ftc.RelationName;
                }
                num++;
                ftc.ColumnNum = num;
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Sequence> GetSequenceList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Sequence>();

            string sql = $@"SELECT
    c.relname AS sequence_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    pg_catalog.format_type(s.seqtypid, NULL)::varchar(63) AS data_type,
    s.seqstart AS start_value,
    s.seqmin AS min_value,
    s.seqmax AS max_value,
    s.seqincrement AS increment_by,
    s.seqcycle::text AS cycle,
    s.seqcache AS cache_size,
    pg_catalog.obj_description(c.oid, 'pg_class') AS description,
    pg_catalog.array_to_string(c.relacl, ',') AS access_privileges,
    (SELECT pg_catalog.quote_ident(relname) || '.' || pg_catalog.quote_ident(attname)
     FROM pg_catalog.pg_class cl
     JOIN pg_catalog.pg_depend d ON cl.oid = d.refobjid
     JOIN pg_catalog.pg_attribute a ON (a.attrelid = cl.oid AND a.attnum = d.refobjsubid)
     WHERE d.classid = 'pg_catalog.pg_class'::pg_catalog.regclass
     AND d.refclassid = 'pg_catalog.pg_class'::pg_catalog.regclass
     AND d.objid = c.oid
     AND d.deptype IN ('a', 'i'))::varchar(128) AS owned_by
FROM pg_catalog.pg_sequence s
JOIN pg_catalog.pg_class c ON c.oid = s.seqrelid
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE c.relkind = 'S' AND n.nspname = @schema
ORDER BY sequence_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var se = new Sequence
                    {
                        SequenceName = (string)dr["sequence_name"],
                        Owner = (string)dr["owner"],
                        DataType = (string)dr["data_type"],
                        StartValue = (long)dr["start_value"],
                        MinValue = (long)dr["min_value"],
                        MaxValue = (long)dr["max_value"],
                        IncrementBy = (long)dr["increment_by"],
                        Cycle = (string)dr["cycle"],
                        CacheSize = (long)dr["cache_size"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                        OwnedBy = dr["owned_by"] is DBNull ? null : (string)dr["owned_by"],
                    };
                    list.Add(se);
                }
            }
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Index> GetIndexList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Index>();

            string sql = $@"SELECT
    c.relname AS index_name,
    c2.relname AS table_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    ts.spcname AS tablespace_name,
    CASE c.relpersistence WHEN 'p' THEN 'permanent' WHEN 't' THEN 'temporary' WHEN 'u' THEN 'unlogged' END AS persistence,
    am.amname AS access_method,
    CASE c.relkind WHEN 'I' THEN TRUE ELSE FALSE END::text AS is_partitioned,
    c.relispartition::text AS is_partition,
    c.relhassubclass::text AS has_subclass,
    i.indisunique::text AS is_unique,
    i.indisprimary::text AS is_primary,
    i.indisexclusion::text AS is_exclusion,
    i.indimmediate::text AS immediate,
    i.indisclustered::text AS is_clustered,
    i.indisvalid::text AS is_valid,
    pg_catalog.pg_get_indexdef(c.oid) AS definition,
    pg_catalog.obj_description(c.oid, 'pg_class') AS description,
    pg_catalog.array_to_string(c.reloptions, ',') AS options
FROM pg_catalog.pg_class c
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
LEFT JOIN pg_catalog.pg_index i ON i.indexrelid = c.oid
LEFT JOIN pg_catalog.pg_class c2 ON i.indrelid = c2.oid
LEFT JOIN pg_catalog.pg_am am ON am.oid = c.relam
LEFT JOIN pg_catalog.pg_tablespace ts ON ts.oid = c.reltablespace
WHERE c.relkind IN ('i','I')
AND n.nspname = @schema
ORDER BY table_name, index_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ix = new Index
                    {
                        IndexName = (string)dr["index_name"],
                        TableName = (string)dr["table_name"],
                        Owner = (string)dr["owner"],
                        TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"],
                        Persistence = (string)dr["persistence"],
                        AccessMethod = dr["access_method"] is DBNull ? null : (string)dr["access_method"],
                        IsPartitioned = (string)dr["is_partitioned"],
                        IsPartition = (string)dr["is_partition"],
                        HasSubclass = (string)dr["has_subclass"],
                        IsUnique = (string)dr["is_unique"],
                        IsPrimary = (string)dr["is_primary"],
                        IsExclusion = (string)dr["is_exclusion"],
                        Immediate = (string)dr["immediate"],
                        IsClustered = (string)dr["is_clustered"],
                        IsValid = (string)dr["is_valid"],
                        Definition = (string)dr["definition"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        Options = dr["options"] is DBNull ? null : (string)dr["options"],
                    };
                    list.Add(ix);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<IndexColumn> GetIndexColumnList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<IndexColumn>();

            string sql = $@"SELECT
    c.relname AS index_name,
    a.attname AS column_name,
    pg_catalog.pg_get_userbyid(c.relowner) AS owner,
    a.attnum AS column_num,
    pg_catalog.format_type(a.atttypid, a.atttypmod)::varchar(128) AS data_type,
    (SELECT c.collname FROM pg_catalog.pg_collation c, pg_catalog.pg_type t
     WHERE c.oid = a.attcollation AND t.oid = a.atttypid AND a.attcollation <> t.typcollation) AS collation,
    (NOT a.attnotnull)::text AS nullable,
    (SELECT pg_catalog.pg_get_expr(d.adbin, d.adrelid, true)
     FROM pg_catalog.pg_attrdef d
     WHERE d.adrelid = a.attrelid AND d.adnum = a.attnum AND a.atthasdef) AS data_default,
    CASE a.attidentity WHEN 'a' THEN 'always' WHEN 'd' THEN 'by default' ELSE NULL END::varchar(10) AS identity,
    {(pgVersion.IsGreaterOrEqual(12, 0) ? "CASE a.attgenerated WHEN 's' THEN 'stored' ELSE NULL END::varchar(6)" : "NULL")} AS generated,
    CASE a.attstorage WHEN 'p' THEN 'plain' WHEN 'e' THEN 'external' WHEN 'm' THEN 'main' WHEN 'x' THEN 'extended' END::varchar(8) AS storage,
    {(pgVersion.IsGreaterOrEqual(14, 0) ? "CASE a.attcompression WHEN 'p' THEN 'pglz' WHEN 'l' THEN 'LZ4' ELSE NULL END::varchar(4)" : "NULL")} AS compression,
    CASE WHEN a.attstattarget = -1 THEN NULL ELSE a.attstattarget END AS statistics_target,
    a.attislocal::text AS is_local,
    a.attinhcount AS inheritance_count,
    pg_catalog.col_description(a.attrelid, a.attnum) AS description,
    pg_catalog.array_to_string(a.attacl, ',') AS access_privileges,
    pg_catalog.array_to_string(a.attoptions, ',') AS options
FROM pg_catalog.pg_attribute a
JOIN pg_catalog.pg_class c ON c.oid = a.attrelid
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE c.relkind IN ('i','I') AND a.attnum > 0 AND NOT a.attisdropped
AND n.nspname = @schema
ORDER BY index_name, column_num";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ic = new IndexColumn
                    {
                        RelationName = (string)dr["index_name"],
                        ColumnName = (string)dr["column_name"],
                        Owner = (string)dr["owner"],
                        ColumnNum = (short)dr["column_num"],
                        DataType = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_type"], schema),
                        Collation = dr["collation"] is DBNull ? null : (string)dr["collation"],
                        Nullable = (string)dr["nullable"],
                        DataDefault = dr["data_default"] is DBNull ? null : NormalizeHelpers.GetItemWithoutSchemaName((string)dr["data_default"], schema),
                        Identity = dr["identity"] is DBNull ? null : (string)dr["identity"],
                        Generated = dr["generated"] is DBNull ? null : (string)dr["generated"],
                        Storage = (string)dr["storage"],
                        Compression = dr["compression"] is DBNull ? null : (string)dr["compression"],
                        StatisticsTarget = dr["statistics_target"] is DBNull ? null : (int?)dr["statistics_target"],
                        IsLocal = (string)dr["is_local"],
                        InheritanceCount = (int)dr["inheritance_count"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                        Options = dr["options"] is DBNull ? null : (string)dr["options"],
                    };
                    list.Add(ic);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Trigger> GetTriggerList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Trigger>();

            string sql = $@"SELECT
    c.relname AS table_name,
    t.tgname AS trigger_name,
    pg_catalog.pg_get_triggerdef(t.oid, true) AS definition,
    CASE t.tgenabled WHEN 'D' THEN false ELSE true END::text AS enabled
FROM pg_catalog.pg_trigger t
JOIN pg_catalog.pg_class c ON c.oid = t.tgrelid
JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE NOT t.tgisinternal AND n.nspname = @schema
ORDER BY table_name, trigger_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var tr = new Trigger
                    {
                        TableName = (string)dr["table_name"],
                        TriggerName = (string)dr["trigger_name"],
                        Definition = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["definition"], schema),
                        Enabled = (string)dr["enabled"],
                    };
                    list.Add(tr);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Function> GetFunctionList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Function>();

            string sql = $@"SELECT
    p.proname AS function_name,
    md5(pg_catalog.pg_get_function_identity_arguments(p.oid))::varchar(32) AS identity_arguments_hash,
    pg_catalog.pg_get_function_identity_arguments(p.oid) AS identity_arguments,
    pg_catalog.pg_get_function_arguments(p.oid) AS argument_data_types,
    pg_catalog.pg_get_function_result(p.oid)::varchar(128) AS result_data_type,
    pg_catalog.pg_get_userbyid(p.proowner) AS owner,
    CASE p.prokind WHEN 'a' THEN 'agg' WHEN 'w' THEN 'window' WHEN 'p' THEN 'proc' ELSE 'func' END::varchar(6) AS function_type,
    CASE p.provolatile WHEN 'i' THEN 'immutable' WHEN 's' THEN 'stable' WHEN 'v' THEN 'volatile' END::varchar(9) AS volatility,
    CASE p.proparallel WHEN 'r' THEN 'restricted' WHEN 's' THEN 'safe' WHEN 'u' THEN 'unsafe' END::varchar(10) AS parallel,
    CASE WHEN p.prosecdef THEN 'definer' ELSE 'invoker' END::varchar(7) AS security,
    l.lanname AS language,
    {(pgVersion.IsGreaterOrEqual(14, 0) ? "COALESCE(pg_catalog.pg_get_function_sqlbody(p.oid), p.prosrc)" : "p.prosrc")} AS source_code,
    CASE WHEN p.prokind <> 'a' THEN pg_catalog.pg_get_functiondef(p.oid) ELSE NULL END AS definition,
    pg_catalog.obj_description(p.oid, 'pg_proc') AS description,
    pg_catalog.array_to_string(p.proacl, ',') AS access_privileges
FROM pg_catalog.pg_proc p
JOIN pg_catalog.pg_namespace n ON n.oid = p.pronamespace
LEFT JOIN pg_catalog.pg_language l ON l.oid = p.prolang
WHERE n.nspname = @schema
ORDER BY function_name, identity_arguments";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var fu = new Function
                    {
                        FunctionName = (string)dr["function_name"],
                        IdentityArgumentsHash = (string)dr["identity_arguments_hash"],
                        IdentityArguments = (string)dr["identity_arguments"],
                        ArgumentDataTypes = (string)dr["argument_data_types"],
                        ResultDataType = dr["result_data_type"] is DBNull ? null : NormalizeHelpers.GetItemWithoutSchemaName((string)dr["result_data_type"], schema),
                        Owner = (string)dr["owner"],
                        FunctionType = (string)dr["function_type"],
                        Volatility = (string)dr["volatility"],
                        Parallel = (string)dr["parallel"],
                        Security = (string)dr["security"],
                        Language = (string)dr["language"],
                        SourceCode = (string)dr["source_code"],
                        Definition = dr["definition"] is DBNull ? null : NormalizeHelpers.GetItemWithoutSchemaName((string)dr["definition"], schema),
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                    };
                    list.Add(fu);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Domain.Domain> GetDomainList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Domain.Domain>();

            string sql = $@"SELECT
    t.typname AS domain_name,
    pg_catalog.pg_get_userbyid(t.typowner) AS owner,
    pg_catalog.format_type(t.typbasetype, t.typtypmod)::varchar(128) AS data_type,
    (SELECT c.collname FROM pg_catalog.pg_collation c, pg_catalog.pg_type bt
     WHERE c.oid = t.typcollation AND bt.oid = t.typbasetype AND t.typcollation <> bt.typcollation) AS collation,
    (NOT t.typnotnull)::text AS nullable,
    t.typdefault AS data_default,
    pg_catalog.array_to_string(ARRAY(
     SELECT pg_catalog.pg_get_constraintdef(r.oid, true) FROM pg_catalog.pg_constraint r WHERE t.oid = r.contypid
     ), ' ') AS check_constraint,
    pg_catalog.obj_description(t.oid, 'pg_type') AS description,
    pg_catalog.array_to_string(t.typacl, ',') AS access_privileges
FROM pg_catalog.pg_type t
JOIN pg_catalog.pg_namespace n ON n.oid = t.typnamespace
WHERE t.typtype = 'd' AND n.nspname = @schema
ORDER BY domain_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var dm = new Domain.Domain
                    {
                        DomainName = (string)dr["domain_name"],
                        Owner = (string)dr["owner"],
                        DataType = (string)dr["data_type"],
                        Collation = dr["collation"] is DBNull ? null : (string)dr["collation"],
                        Nullable = (string)dr["nullable"],
                        DataDefault = dr["data_default"] is DBNull ? null : (string)dr["data_default"],
                        CheckConstraint = dr["check_constraint"] is DBNull ? null : (string)dr["check_constraint"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                    };
                    list.Add(dm);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<PostgresType> GetTypeList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<PostgresType>();

            string sql = $@"SELECT
    t.typname AS type_name,
    pg_catalog.format_type(t.oid, NULL)::varchar(128) AS format_type_name,
    pg_catalog.pg_get_userbyid(t.typowner) AS owner,
    CASE t.typtype WHEN 'b' THEN 'base' WHEN 'c' THEN 'composite' WHEN 'd' THEN 'domain' WHEN 'e' THEN 'enum'
     WHEN 'p' THEN 'pseudo-type' WHEN 'r' THEN 'range' WHEN 'm' THEN 'multirange' ELSE NULL END::varchar(11) AS type_type,
    CASE WHEN t.typrelid != 0 THEN CAST('tuple' AS pg_catalog.text)
     WHEN t.typlen < 0 THEN CAST('var' AS pg_catalog.text)
     ELSE CAST(t.typlen AS pg_catalog.text) END::varchar(32) AS type_size,
    pg_catalog.array_to_string(ARRAY(
     SELECT e.enumlabel FROM pg_catalog.pg_enum e WHERE e.enumtypid = t.oid ORDER BY e.enumsortorder
     ), ',') AS elements,
    pg_catalog.obj_description(t.oid, 'pg_type') AS description,
    pg_catalog.array_to_string(t.typacl, ',') AS access_privileges
FROM pg_catalog.pg_type t
JOIN pg_catalog.pg_namespace n ON n.oid = t.typnamespace
WHERE (t.typrelid = 0 OR (SELECT c.relkind = 'c' FROM pg_catalog.pg_class c WHERE c.oid = t.typrelid))
AND NOT EXISTS(SELECT 1 FROM pg_catalog.pg_type el WHERE el.oid = t.typelem AND el.typarray = t.oid)
AND t.typtype <> 'd'
AND n.nspname = @schema
ORDER BY type_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ty = new PostgresType
                    {
                        TypeName = (string)dr["type_name"],
                        FormatTypeName = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["format_type_name"], schema),
                        Owner = (string)dr["owner"],
                        TypeType = dr["type_type"] is DBNull ? null : (string)dr["type_type"],
                        TypeSize = (string)dr["type_size"],
                        Elements = dr["elements"] is DBNull ? null : (string)dr["elements"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                        AccessPrivileges = dr["access_privileges"] is DBNull ? null : (string)dr["access_privileges"],
                    };
                    list.Add(ty);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Extension> GetExtensionList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Extension>();

            string sql = $@"SELECT
    e.extname AS extension_name,
    pg_catalog.pg_get_userbyid(e.extowner) AS owner,
    e.extversion AS version,
    pg_catalog.obj_description(e.oid, 'pg_extension') AS description
FROM pg_catalog.pg_extension e
JOIN pg_catalog.pg_namespace n ON n.oid = e.extnamespace
WHERE n.nspname = @schema
ORDER BY extension_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ex = new Extension
                    {
                        ExtensionName = (string)dr["extension_name"],
                        Owner = (string)dr["owner"],
                        Version = (string)dr["version"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                    };
                    list.Add(ex);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="pgVersion"></param>
        /// <returns></returns>
        public List<Statistics> GetStatisticsList(NpgsqlConnection conn, string schema, Version pgVersion)
        {
            var list = new List<Statistics>();

            string sql = $@"SELECT
    es.stxname AS statistics_name,
    pg_catalog.pg_get_userbyid(es.stxowner) AS owner,
    {(pgVersion.IsGreaterOrEqual(14, 0) ? "pg_catalog.format('%s FROM %s', pg_catalog.pg_get_statisticsobjdef_columns(es.oid), es.stxrelid::pg_catalog.regclass)" : "es.stxrelid::pg_catalog.regclass")} AS definition,
    CASE WHEN 'd' = any(es.stxkind) THEN 'defined' END AS ndistinct,
    CASE WHEN 'f' = any(es.stxkind) THEN 'defined' END AS dependencies,
    CASE WHEN 'm' = any(es.stxkind) THEN 'defined' END AS mcv,
    {(pgVersion.IsGreaterOrEqual(13, 0) ? "es.stxstattarget" : "NULL::integer")} AS statistics_target,
    pg_catalog.obj_description(es.oid, 'pg_statistic_ext') AS description
FROM pg_catalog.pg_statistic_ext es
JOIN pg_catalog.pg_namespace n ON n.oid = es.stxnamespace
WHERE n.nspname = @schema
ORDER BY statistics_name";

            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("schema", schema);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var st = new Statistics
                    {
                        StatisticsName = (string)dr["statistics_name"],
                        Owner = (string)dr["owner"],
                        Definition = NormalizeHelpers.GetItemWithoutSchemaName((string)dr["definition"], schema),
                        NDistinct = dr["ndistinct"] is DBNull ? null : (string)dr["ndistinct"],
                        Dependencies = dr["dependencies"] is DBNull ? null : (string)dr["dependencies"],
                        MCV = dr["mcv"] is DBNull ? null : (string)dr["mcv"],
                        StatisticsTarget = dr["statistics_target"] is DBNull ? null : (int?)dr["statistics_target"],
                        Description = dr["description"] is DBNull ? null : (string)dr["description"],
                    };
                    list.Add(st);
                }
            }
            return list;
        }

    }
}
