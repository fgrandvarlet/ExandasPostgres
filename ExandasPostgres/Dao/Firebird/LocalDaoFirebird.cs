using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao.Firebird
{
    public class LocalDaoFirebird : AbstractDaoFirebird, ILocalDao
    {
        public LocalDaoFirebird(string connectionString) : base(connectionString)
        {
        }

        protected override FbCommand CreateCommand(Criteria criteria)
        {
            throw new NotImplementedException();
        }

        private static string GetPrefix(SchemaType schemaType)
        {
            string result;
            switch (schemaType)
            {
                case SchemaType.Source:
                    result = "src";
                    break;
                case SchemaType.Target:
                    result = "tgt";
                    break;
                default:
                    throw new ArgumentException(nameof(schemaType));
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTableList(FbTransaction tran, SchemaType schemaType, List<Table> list)
        {
            var tableName = string.Format("{0}_table", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@table_name, @owner, @tablespace_name, @persistence, @access_method, @is_partitioned, @is_partition," +
                " @has_subclass, @row_security, @force_row_security, @description, @access_privileges, @options, @partition_key, @partition_bound)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ta in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("table_name", ta.TableName);
                cmd.Parameters.AddWithValue("owner", ta.Owner);
                cmd.Parameters.AddWithValue("tablespace_name", ta.TablespaceName);
                cmd.Parameters.AddWithValue("persistence", ta.Persistence);
                cmd.Parameters.AddWithValue("access_method", ta.AccessMethod);
                cmd.Parameters.AddWithValue("is_partitioned", ta.IsPartitioned);
                cmd.Parameters.AddWithValue("is_partition", ta.IsPartition);
                cmd.Parameters.AddWithValue("has_subclass", ta.HasSubclass);
                cmd.Parameters.AddWithValue("row_security", ta.RowSecurity);
                cmd.Parameters.AddWithValue("force_row_security", ta.ForceRowSecurity);
                cmd.Parameters.AddWithValue("description", ta.Description);
                cmd.Parameters.AddWithValue("access_privileges", ta.AccessPrivileges);
                cmd.Parameters.AddWithValue("options", ta.Options);
                cmd.Parameters.AddWithValue("partition_key", ta.PartitionKey);
                cmd.Parameters.AddWithValue("partition_bound", ta.PartitionBound);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTableColumnList(FbTransaction tran, SchemaType schemaType, List<TableColumn> list)
        {
            var tableName = string.Format("{0}_table_column", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@table_name, @column_name, @owner, @column_num, @data_type, @collation, @nullable, @data_default," +
                " @identity, @generated, @storage, @compression, @statistics_target, @is_local, @inheritance_count, @description, @access_privileges, @options)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var tc in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("table_name", tc.RelationName);
                cmd.Parameters.AddWithValue("column_name", tc.ColumnName);
                cmd.Parameters.AddWithValue("owner", tc.Owner);
                cmd.Parameters.AddWithValue("column_num", tc.ColumnNum);
                cmd.Parameters.AddWithValue("data_type", tc.DataType);
                cmd.Parameters.AddWithValue("collation", tc.Collation);
                cmd.Parameters.AddWithValue("nullable", tc.Nullable);
                cmd.Parameters.AddWithValue("data_default", tc.DataDefault);
                cmd.Parameters.AddWithValue("identity", tc.Identity);
                cmd.Parameters.AddWithValue("generated", tc.Generated);
                cmd.Parameters.AddWithValue("storage", tc.Storage);
                cmd.Parameters.AddWithValue("compression", tc.Compression);
                cmd.Parameters.AddWithValue("statistics_target", tc.StatisticsTarget);
                cmd.Parameters.AddWithValue("is_local", tc.IsLocal);
                cmd.Parameters.AddWithValue("inheritance_count", tc.InheritanceCount);
                cmd.Parameters.AddWithValue("description", tc.Description);
                cmd.Parameters.AddWithValue("access_privileges", tc.AccessPrivileges);
                cmd.Parameters.AddWithValue("options", tc.Options);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadPrimaryKeyList(FbTransaction tran, SchemaType schemaType, List<PrimaryKey> list)
        {
            var tableName = string.Format("{0}_primary_key", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @deferrable, @deferred, @definition)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var pk in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", pk.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", pk.TableName);
                cmd.Parameters.AddWithValue("deferrable", pk.Deferrable);
                cmd.Parameters.AddWithValue("deferred", pk.Deferred);
                cmd.Parameters.AddWithValue("definition", pk.Definition);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadUniqueList(FbTransaction tran, SchemaType schemaType, List<Unique> list)
        {
            var tableName = string.Format("{0}_unique", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @deferrable, @deferred, @definition)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var un in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", un.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", un.TableName);
                cmd.Parameters.AddWithValue("deferrable", un.Deferrable);
                cmd.Parameters.AddWithValue("deferred", un.Deferred);
                cmd.Parameters.AddWithValue("definition", un.Definition);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadForeignKeyList(FbTransaction tran, SchemaType schemaType, List<ForeignKey> list)
        {
            var tableName = string.Format("{0}_foreign_key", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @deferrable, @deferred, @validated," +
                " @index_name, @referenced_table_name, @update_action, @delete_action, @match_type, @definition)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var fk in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", fk.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", fk.TableName);
                cmd.Parameters.AddWithValue("deferrable", fk.Deferrable);
                cmd.Parameters.AddWithValue("deferred", fk.Deferred);
                cmd.Parameters.AddWithValue("validated", fk.Validated);
                cmd.Parameters.AddWithValue("index_name", fk.IndexName);
                cmd.Parameters.AddWithValue("referenced_table_name", fk.ReferencedTableName);
                cmd.Parameters.AddWithValue("update_action", fk.UpdateAction);
                cmd.Parameters.AddWithValue("delete_action", fk.DeleteAction);
                cmd.Parameters.AddWithValue("match_type", fk.MatchType);
                cmd.Parameters.AddWithValue("definition", fk.Definition);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadCheckList(FbTransaction tran, SchemaType schemaType, List<Check> list)
        {
            var tableName = string.Format("{0}_check", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @validated, @definition)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ck in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", ck.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", ck.TableName);
                cmd.Parameters.AddWithValue("validated", ck.Validated);
                cmd.Parameters.AddWithValue("definition", ck.Definition);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadExclusionList(FbTransaction tran, SchemaType schemaType, List<Exclusion> list)
        {
            var tableName = string.Format("{0}_exclusion", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @deferrable, @deferred, @definition)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ex in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", ex.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", ex.TableName);
                cmd.Parameters.AddWithValue("deferrable", ex.Deferrable);
                cmd.Parameters.AddWithValue("deferred", ex.Deferred);
                cmd.Parameters.AddWithValue("definition", ex.Definition);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadViewList(FbTransaction tran, SchemaType schemaType, List<View> list)
        {
            var tableName = string.Format("{0}_view", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@view_name, @owner, @persistence," +
                " @definition, @description, @access_privileges, @options)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var vi in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("view_name", vi.ViewName);
                cmd.Parameters.AddWithValue("owner", vi.Owner);
                cmd.Parameters.AddWithValue("persistence", vi.Persistence);
                cmd.Parameters.AddWithValue("definition", vi.Definition);
                cmd.Parameters.AddWithValue("description", vi.Description);
                cmd.Parameters.AddWithValue("access_privileges", vi.AccessPrivileges);
                cmd.Parameters.AddWithValue("options", vi.Options);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadViewColumnList(FbTransaction tran, SchemaType schemaType, List<ViewColumn> list)
        {
            var tableName = string.Format("{0}_view_column", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@view_name, @column_name, @owner, @column_num, @data_type, @collation, @nullable, @data_default," +
                " @identity, @generated, @storage, @compression, @statistics_target, @is_local, @inheritance_count, @description, @access_privileges, @options)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var vc in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("view_name", vc.RelationName);
                cmd.Parameters.AddWithValue("column_name", vc.ColumnName);
                cmd.Parameters.AddWithValue("owner", vc.Owner);
                cmd.Parameters.AddWithValue("column_num", vc.ColumnNum);
                cmd.Parameters.AddWithValue("data_type", vc.DataType);
                cmd.Parameters.AddWithValue("collation", vc.Collation);
                cmd.Parameters.AddWithValue("nullable", vc.Nullable);
                cmd.Parameters.AddWithValue("data_default", vc.DataDefault);
                cmd.Parameters.AddWithValue("identity", vc.Identity);
                cmd.Parameters.AddWithValue("generated", vc.Generated);
                cmd.Parameters.AddWithValue("storage", vc.Storage);
                cmd.Parameters.AddWithValue("compression", vc.Compression);
                cmd.Parameters.AddWithValue("statistics_target", vc.StatisticsTarget);
                cmd.Parameters.AddWithValue("is_local", vc.IsLocal);
                cmd.Parameters.AddWithValue("inheritance_count", vc.InheritanceCount);
                cmd.Parameters.AddWithValue("description", vc.Description);
                cmd.Parameters.AddWithValue("access_privileges", vc.AccessPrivileges);
                cmd.Parameters.AddWithValue("options", vc.Options);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadMaterializedViewList(FbTransaction tran, SchemaType schemaType, List<MaterializedView> list)
        {
            var tableName = string.Format("{0}_materialized_view", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@materialized_view_name, @owner, @tablespace_name, @persistence, @access_method," +
                " @definition, @description, @access_privileges, @options)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var mv in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("materialized_view_name", mv.MaterializedViewName);
                cmd.Parameters.AddWithValue("owner", mv.Owner);
                cmd.Parameters.AddWithValue("tablespace_name", mv.TablespaceName);
                cmd.Parameters.AddWithValue("persistence", mv.Persistence);
                cmd.Parameters.AddWithValue("access_method", mv.AccessMethod);
                cmd.Parameters.AddWithValue("definition", mv.Definition);
                cmd.Parameters.AddWithValue("description", mv.Description);
                cmd.Parameters.AddWithValue("access_privileges", mv.AccessPrivileges);
                cmd.Parameters.AddWithValue("options", mv.Options);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadMaterializedViewColumnList(FbTransaction tran, SchemaType schemaType, List<MaterializedViewColumn> list)
        {
            var tableName = string.Format("{0}_materialized_view_column", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@materialized_view_name, @column_name, @owner, @column_num, @data_type, @collation, @nullable, @data_default," +
                " @identity, @generated, @storage, @compression, @statistics_target, @is_local, @inheritance_count, @description, @access_privileges, @options)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var mvc in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("materialized_view_name", mvc.RelationName);
                cmd.Parameters.AddWithValue("column_name", mvc.ColumnName);
                cmd.Parameters.AddWithValue("owner", mvc.Owner);
                cmd.Parameters.AddWithValue("column_num", mvc.ColumnNum);
                cmd.Parameters.AddWithValue("data_type", mvc.DataType);
                cmd.Parameters.AddWithValue("collation", mvc.Collation);
                cmd.Parameters.AddWithValue("nullable", mvc.Nullable);
                cmd.Parameters.AddWithValue("data_default", mvc.DataDefault);
                cmd.Parameters.AddWithValue("identity", mvc.Identity);
                cmd.Parameters.AddWithValue("generated", mvc.Generated);
                cmd.Parameters.AddWithValue("storage", mvc.Storage);
                cmd.Parameters.AddWithValue("compression", mvc.Compression);
                cmd.Parameters.AddWithValue("statistics_target", mvc.StatisticsTarget);
                cmd.Parameters.AddWithValue("is_local", mvc.IsLocal);
                cmd.Parameters.AddWithValue("inheritance_count", mvc.InheritanceCount);
                cmd.Parameters.AddWithValue("description", mvc.Description);
                cmd.Parameters.AddWithValue("access_privileges", mvc.AccessPrivileges);
                cmd.Parameters.AddWithValue("options", mvc.Options);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadForeignTableList(FbTransaction tran, SchemaType schemaType, List<ForeignTable> list)
        {
            var tableName = string.Format("{0}_foreign_table", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@foreign_table_name, @owner, @server, @fdw_options, @persistence," +
                " @is_partition, @has_subclass, @row_security, @force_row_security, @description, @access_privileges)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ft in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("foreign_table_name", ft.ForeignTableName);
                cmd.Parameters.AddWithValue("owner", ft.Owner);
                cmd.Parameters.AddWithValue("server", ft.Server);
                cmd.Parameters.AddWithValue("fdw_options", ft.FdwOptions);
                cmd.Parameters.AddWithValue("persistence", ft.Persistence);
                cmd.Parameters.AddWithValue("is_partition", ft.IsPartition);
                cmd.Parameters.AddWithValue("has_subclass", ft.HasSubclass);
                cmd.Parameters.AddWithValue("row_security", ft.RowSecurity);
                cmd.Parameters.AddWithValue("force_row_security", ft.ForceRowSecurity);
                cmd.Parameters.AddWithValue("description", ft.Description);
                cmd.Parameters.AddWithValue("access_privileges", ft.AccessPrivileges);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadForeignTableColumnList(FbTransaction tran, SchemaType schemaType, List<ForeignTableColumn> list)
        {
            var tableName = string.Format("{0}_foreign_table_column", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@foreign_table_name, @column_name, @owner, @column_num, @data_type, @collation, @nullable, @data_default," +
                " @identity, @generated, @storage, @compression, @statistics_target, @is_local, @inheritance_count, @description, @access_privileges, @options, @fdw_options)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ftc in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("foreign_table_name", ftc.RelationName);
                cmd.Parameters.AddWithValue("column_name", ftc.ColumnName);
                cmd.Parameters.AddWithValue("owner", ftc.Owner);
                cmd.Parameters.AddWithValue("column_num", ftc.ColumnNum);
                cmd.Parameters.AddWithValue("data_type", ftc.DataType);
                cmd.Parameters.AddWithValue("collation", ftc.Collation);
                cmd.Parameters.AddWithValue("nullable", ftc.Nullable);
                cmd.Parameters.AddWithValue("data_default", ftc.DataDefault);
                cmd.Parameters.AddWithValue("identity", ftc.Identity);
                cmd.Parameters.AddWithValue("generated", ftc.Generated);
                cmd.Parameters.AddWithValue("storage", ftc.Storage);
                cmd.Parameters.AddWithValue("compression", ftc.Compression);
                cmd.Parameters.AddWithValue("statistics_target", ftc.StatisticsTarget);
                cmd.Parameters.AddWithValue("is_local", ftc.IsLocal);
                cmd.Parameters.AddWithValue("inheritance_count", ftc.InheritanceCount);
                cmd.Parameters.AddWithValue("description", ftc.Description);
                cmd.Parameters.AddWithValue("access_privileges", ftc.AccessPrivileges);
                cmd.Parameters.AddWithValue("options", ftc.Options);
                cmd.Parameters.AddWithValue("fdw_options", ftc.FdwOptions);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadSequenceList(FbTransaction tran, SchemaType schemaType, List<Sequence> list)
        {
            var tableName = string.Format("{0}_sequence", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@sequence_name, @owner, @data_type, @start_value, @min_value, @max_value," +
                " @increment_by, @cycle, @cache_size, @description, @access_privileges, @owned_by)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var se in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("sequence_name", se.SequenceName);
                cmd.Parameters.AddWithValue("owner", se.Owner);
                cmd.Parameters.AddWithValue("data_type", se.DataType);
                cmd.Parameters.AddWithValue("start_value", se.StartValue);
                cmd.Parameters.AddWithValue("min_value", se.MinValue);
                cmd.Parameters.AddWithValue("max_value", se.MaxValue);
                cmd.Parameters.AddWithValue("increment_by", se.IncrementBy);
                cmd.Parameters.AddWithValue("cycle", se.Cycle);
                cmd.Parameters.AddWithValue("cache_size", se.CacheSize);
                cmd.Parameters.AddWithValue("description", se.Description);
                cmd.Parameters.AddWithValue("access_privileges", se.AccessPrivileges);
                cmd.Parameters.AddWithValue("owned_by", se.OwnedBy);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadIndexList(FbTransaction tran, SchemaType schemaType, List<Index> list)
        {
            var tableName = string.Format("{0}_index", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@index_name, @table_name, @owner, @tablespace_name, @persistence," +
                " @access_method, @is_partitioned, @is_partition, @has_subclass, @is_unique, @is_primary, @is_exclusion," +
                " @immediate, @is_clustered, @is_valid, @definition, @description, @options)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ix in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("index_name", ix.IndexName);
                cmd.Parameters.AddWithValue("table_name", ix.TableName);
                cmd.Parameters.AddWithValue("owner", ix.Owner);
                cmd.Parameters.AddWithValue("tablespace_name", ix.TablespaceName);
                cmd.Parameters.AddWithValue("persistence", ix.Persistence);
                cmd.Parameters.AddWithValue("access_method", ix.AccessMethod);
                cmd.Parameters.AddWithValue("is_partitioned", ix.IsPartitioned);
                cmd.Parameters.AddWithValue("is_partition", ix.IsPartition);
                cmd.Parameters.AddWithValue("has_subclass", ix.HasSubclass);
                cmd.Parameters.AddWithValue("is_unique", ix.IsUnique);
                cmd.Parameters.AddWithValue("is_primary", ix.IsPrimary);
                cmd.Parameters.AddWithValue("is_exclusion", ix.IsExclusion);
                cmd.Parameters.AddWithValue("immediate", ix.Immediate);
                cmd.Parameters.AddWithValue("is_clustered", ix.IsClustered);
                cmd.Parameters.AddWithValue("is_valid", ix.IsValid);
                cmd.Parameters.AddWithValue("definition", ix.Definition);
                cmd.Parameters.AddWithValue("description", ix.Description);
                cmd.Parameters.AddWithValue("options", ix.Options);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadIndexColumnList(FbTransaction tran, SchemaType schemaType, List<IndexColumn> list)
        {
            var tableName = string.Format("{0}_index_column", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@index_name, @column_name, @owner, @column_num, @data_type, @collation, @nullable, @data_default," +
                " @identity, @generated, @storage, @compression, @statistics_target, @is_local, @inheritance_count, @description, @access_privileges, @options)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ic in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("index_name", ic.RelationName);
                cmd.Parameters.AddWithValue("column_name", ic.ColumnName);
                cmd.Parameters.AddWithValue("owner", ic.Owner);
                cmd.Parameters.AddWithValue("column_num", ic.ColumnNum);
                cmd.Parameters.AddWithValue("data_type", ic.DataType);
                cmd.Parameters.AddWithValue("collation", ic.Collation);
                cmd.Parameters.AddWithValue("nullable", ic.Nullable);
                cmd.Parameters.AddWithValue("data_default", ic.DataDefault);
                cmd.Parameters.AddWithValue("identity", ic.Identity);
                cmd.Parameters.AddWithValue("generated", ic.Generated);
                cmd.Parameters.AddWithValue("storage", ic.Storage);
                cmd.Parameters.AddWithValue("compression", ic.Compression);
                cmd.Parameters.AddWithValue("statistics_target", ic.StatisticsTarget);
                cmd.Parameters.AddWithValue("is_local", ic.IsLocal);
                cmd.Parameters.AddWithValue("inheritance_count", ic.InheritanceCount);
                cmd.Parameters.AddWithValue("description", ic.Description);
                cmd.Parameters.AddWithValue("access_privileges", ic.AccessPrivileges);
                cmd.Parameters.AddWithValue("options", ic.Options);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTriggerList(FbTransaction tran, SchemaType schemaType, List<Trigger> list)
        {
            var tableName = string.Format("{0}_trigger", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@table_name, @trigger_name, @definition, @enabled)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var tr in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("table_name", tr.TableName);
                cmd.Parameters.AddWithValue("trigger_name", tr.TriggerName);
                cmd.Parameters.AddWithValue("definition", tr.Definition);
                cmd.Parameters.AddWithValue("enabled", tr.Enabled);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadFunctionList(FbTransaction tran, SchemaType schemaType, List<Function> list)
        {
            var tableName = string.Format("{0}_function", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@function_name, @identity_arguments_hash, @identity_arguments, @argument_data_types, @result_data_type," +
                " @owner, @function_type, @volatility, @parallel, @security, @language, @source_code, @definition, @description, @access_privileges)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var fu in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("function_name", fu.FunctionName);
                cmd.Parameters.AddWithValue("identity_arguments_hash", fu.IdentityArgumentsHash);
                cmd.Parameters.AddWithValue("identity_arguments", fu.IdentityArguments);
                cmd.Parameters.AddWithValue("argument_data_types", fu.ArgumentDataTypes);
                cmd.Parameters.AddWithValue("result_data_type", fu.ResultDataType);
                cmd.Parameters.AddWithValue("owner", fu.Owner);
                cmd.Parameters.AddWithValue("function_type", fu.FunctionType);
                cmd.Parameters.AddWithValue("volatility", fu.Volatility);
                cmd.Parameters.AddWithValue("parallel", fu.Parallel);
                cmd.Parameters.AddWithValue("security", fu.Security);
                cmd.Parameters.AddWithValue("language", fu.Language);
                cmd.Parameters.AddWithValue("source_code", fu.SourceCode);
                cmd.Parameters.AddWithValue("definition", fu.Definition);
                cmd.Parameters.AddWithValue("description", fu.Description);
                cmd.Parameters.AddWithValue("access_privileges", fu.AccessPrivileges);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadDomainList(FbTransaction tran, SchemaType schemaType, List<Domain.Domain> list)
        {
            var tableName = string.Format("{0}_domain", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@domain_name, @owner, @data_type, @collation, @nullable," +
                " @data_default, @check_constraint, @description, @access_privileges)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var dm in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("domain_name", dm.DomainName);
                cmd.Parameters.AddWithValue("owner", dm.Owner); 
                cmd.Parameters.AddWithValue("data_type", dm.DataType);
                cmd.Parameters.AddWithValue("collation", dm.Collation);
                cmd.Parameters.AddWithValue("nullable", dm.Nullable);
                cmd.Parameters.AddWithValue("data_default", dm.DataDefault);
                cmd.Parameters.AddWithValue("check_constraint", dm.CheckConstraint);
                cmd.Parameters.AddWithValue("description", dm.Description);
                cmd.Parameters.AddWithValue("access_privileges", dm.AccessPrivileges);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTypeList(FbTransaction tran, SchemaType schemaType, List<PostgresType> list)
        {
            var tableName = string.Format("{0}_type", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@type_name, @format_type_name, @owner, @type_type, @type_size, @elements, @description, @access_privileges)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ty in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("type_name", ty.TypeName);
                cmd.Parameters.AddWithValue("format_type_name", ty.FormatTypeName);
                cmd.Parameters.AddWithValue("owner", ty.Owner);
                cmd.Parameters.AddWithValue("type_type", ty.TypeType);
                cmd.Parameters.AddWithValue("type_size", ty.TypeSize);
                cmd.Parameters.AddWithValue("elements", ty.Elements);
                cmd.Parameters.AddWithValue("description", ty.Description);
                cmd.Parameters.AddWithValue("access_privileges", ty.AccessPrivileges);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadExtensionList(FbTransaction tran, SchemaType schemaType, List<Extension> list)
        {
            var tableName = string.Format("{0}_extension", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@extension_name, @owner, @version, @description)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ex in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("extension_name", ex.ExtensionName);
                cmd.Parameters.AddWithValue("owner", ex.Owner);
                cmd.Parameters.AddWithValue("version", ex.Version);
                cmd.Parameters.AddWithValue("description", ex.Description);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadStatisticsList(FbTransaction tran, SchemaType schemaType, List<Statistics> list)
        {
            var tableName = string.Format("{0}_statistics", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@statistics_name, @owner, @definition, @ndistinct, @dependencies, @mcv, @statistics_target, @description)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var st in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("statistics_name", st.StatisticsName);
                cmd.Parameters.AddWithValue("owner", st.Owner);
                cmd.Parameters.AddWithValue("definition", st.Definition);
                cmd.Parameters.AddWithValue("ndistinct", st.NDistinct);
                cmd.Parameters.AddWithValue("dependencies", st.Dependencies);
                cmd.Parameters.AddWithValue("mcv", st.MCV);
                cmd.Parameters.AddWithValue("statistics_target", st.StatisticsTarget);
                cmd.Parameters.AddWithValue("description", st.Description);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PurgeMetaDataTables()
        {
            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                FbTransaction tran = conn.BeginTransaction();
                try
                {
                    const string sql = @"SELECT rdb$relation_name AS table_name FROM rdb$relations
                        WHERE rdb$relation_name LIKE 'SRC%' OR rdb$relation_name LIKE 'TGT%'
                        ORDER BY rdb$relation_name";
                    var cmd = new FbCommand(sql, conn, tran);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var tableName = (string)dr["table_name"];
                            (new FbCommand(string.Format("DELETE FROM {0}", tableName), conn, tran)).ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PurgeDeltaReport()
        {
            string sql;
            FbCommand cmd;

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                FbTransaction tran = conn.BeginTransaction();
                try
                {
                    sql = "UPDATE comparison_set SET last_report_time = NULL";
                    cmd = new FbCommand(sql, conn, tran);
                    cmd.ExecuteNonQuery();

                    sql = "DELETE FROM delta_report";
                    cmd = new FbCommand(sql, conn, tran);
                    cmd.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

    }
}
