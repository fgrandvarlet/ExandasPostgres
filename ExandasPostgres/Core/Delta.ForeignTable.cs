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
        private void DeltaForeignTable(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "FOREIGN TABLE";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.foreign_table_name FROM src_foreign_table s" +
                " LEFT JOIN tgt_foreign_table t USING(foreign_table_name)" +
                " WHERE t.foreign_table_name IS NULL" +
                " ORDER BY foreign_table_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["foreign_table_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.foreign_table_name FROM tgt_foreign_table t" +
                " LEFT JOIN src_foreign_table s USING(foreign_table_name)" +
                " WHERE s.foreign_table_name IS NULL" +
                " ORDER BY foreign_table_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["foreign_table_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_foreign_table";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceForeignTable = new ForeignTable
                    {
                        ForeignTableName = (string)dr["foreign_table_name"],
                        Owner = (string)dr["src_owner"],
                        Server = (string)dr["src_server"],
                        FdwOptions = dr["src_fdw_options"] is DBNull ? null : (string)dr["src_fdw_options"],
                        Persistence = (string)dr["src_persistence"],
                        IsPartition = (string)dr["src_is_partition"],
                        HasSubclass = (string)dr["src_has_subclass"],
                        RowSecurity = (string)dr["src_row_security"],
                        ForceRowSecurity = (string)dr["src_force_row_security"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        AccessPrivileges = dr["src_access_privileges"] is DBNull ? null : (string)dr["src_access_privileges"],
                    };
                    var targetForeignTable = new ForeignTable
                    {
                        ForeignTableName = (string)dr["foreign_table_name"],
                        Owner = (string)dr["tgt_owner"],
                        Server = (string)dr["tgt_server"],
                        FdwOptions = dr["tgt_fdw_options"] is DBNull ? null : (string)dr["tgt_fdw_options"],
                        Persistence = (string)dr["tgt_persistence"],
                        IsPartition = (string)dr["tgt_is_partition"],
                        HasSubclass = (string)dr["tgt_has_subclass"],
                        RowSecurity = (string)dr["tgt_row_security"],
                        ForceRowSecurity = (string)dr["tgt_force_row_security"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        AccessPrivileges = dr["tgt_access_privileges"] is DBNull ? null : (string)dr["tgt_access_privileges"],
                    };
                    sourceForeignTable.Compare(targetForeignTable, this._schemaMapping, list);
                }
            }
        }

    }
}
