using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Dao;
using ExandasPostgres.Domain;

namespace ExandasPostgres.Core
{
    public partial class Delta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="list"></param>
        private void DeltaTable(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "TABLE";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name FROM src_table s" +
                " LEFT JOIN tgt_table t USING(table_name)" +
                " WHERE t.table_name IS NULL" +
                " ORDER BY table_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["table_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name FROM tgt_table t" +
                " LEFT JOIN src_table s USING(table_name)" +
                " WHERE s.table_name IS NULL" +
                " ORDER BY table_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["table_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_table";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceTable = new Table
                    {
                        TableName = (string)dr["table_name"],
                        Owner = (string)dr["src_owner"],
                        TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"],
                        Persistence = (string)dr["src_persistence"],
                        AccessMethod = dr["src_access_method"] is DBNull ? null : (string)dr["src_access_method"],
                        IsPartitioned = (string)dr["src_is_partitioned"],
                        IsPartition = (string)dr["src_is_partition"],
                        HasSubclass = (string)dr["src_has_subclass"],
                        RowSecurity = (string)dr["src_row_security"],
                        ForceRowSecurity = (string)dr["src_force_row_security"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        AccessPrivileges = dr["src_access_privileges"] is DBNull ? null : (string)dr["src_access_privileges"],
                        Options = dr["src_options"] is DBNull ? null : (string)dr["src_options"],
                        PartitionKey = dr["src_partition_key"] is DBNull ? null : (string)dr["src_partition_key"],
                        PartitionBound = dr["src_partition_bound"] is DBNull ? null : (string)dr["src_partition_bound"],
                    };
                    var targetTable = new Table
                    {
                        TableName = (string)dr["table_name"],
                        Owner = (string)dr["tgt_owner"],
                        TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"],
                        Persistence = (string)dr["tgt_persistence"],
                        AccessMethod = dr["tgt_access_method"] is DBNull ? null : (string)dr["tgt_access_method"],
                        IsPartitioned = (string)dr["tgt_is_partitioned"],
                        IsPartition = (string)dr["tgt_is_partition"],
                        HasSubclass = (string)dr["tgt_has_subclass"],
                        RowSecurity = (string)dr["tgt_row_security"],
                        ForceRowSecurity = (string)dr["tgt_force_row_security"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        AccessPrivileges = dr["tgt_access_privileges"] is DBNull ? null : (string)dr["tgt_access_privileges"],
                        Options = dr["tgt_options"] is DBNull ? null : (string)dr["tgt_options"],
                        PartitionKey = dr["tgt_partition_key"] is DBNull ? null : (string)dr["tgt_partition_key"],
                        PartitionBound = dr["tgt_partition_bound"] is DBNull ? null : (string)dr["tgt_partition_bound"],
                    };
                    sourceTable.Compare(targetTable, this._schemaMapping, list);
                }
            }
        }

    }
}
