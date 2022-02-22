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
        private void DeltaTableColumn(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "TABLE COLUMN";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.column_name FROM src_table_column s" +
                " LEFT JOIN tgt_table_column t USING(table_name, column_name)" +
                " JOIN common_table USING(table_name)" +
                " WHERE t.column_name IS NULL" +
                " ORDER BY table_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["column_name"], (string)dr["table_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name, t.column_name FROM tgt_table_column t" +
                " LEFT JOIN src_table_column s USING(table_name, column_name)" +
                " JOIN common_table USING(table_name)" +
                " WHERE s.column_name IS NULL" +
                " ORDER BY table_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["column_name"], (string)dr["table_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_table_column";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceTableColumn = new TableColumn
                    {
                        RelationName = (string)dr["table_name"],
                        ColumnName = (string)dr["column_name"],
                        Owner = (string)dr["src_owner"],
                        ColumnNum = (short)dr["src_column_num"],
                        DataType = (string)dr["src_data_type"],
                        Collation = dr["src_collation"] is DBNull ? null : (string)dr["src_collation"],
                        Nullable = (string)dr["src_nullable"],
                        DataDefault = dr["src_data_default"] is DBNull ? null : (string)dr["src_data_default"],
                        Identity = dr["src_identity"] is DBNull ? null : (string)dr["src_identity"],
                        Generated = dr["src_generated"] is DBNull ? null : (string)dr["src_generated"],
                        Storage = (string)dr["src_storage"],
                        Compression = dr["src_compression"] is DBNull ? null : (string)dr["src_compression"],
                        StatisticsTarget = dr["src_statistics_target"] is DBNull ? null : (int?)dr["src_statistics_target"],
                        IsLocal = (string)dr["src_is_local"],
                        InheritanceCount = (int)dr["src_inheritance_count"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        AccessPrivileges = dr["src_access_privileges"] is DBNull ? null : (string)dr["src_access_privileges"],
                        Options = dr["src_options"] is DBNull ? null : (string)dr["src_options"],
                    };
                    var targetTableColumn = new TableColumn
                    {
                        RelationName = (string)dr["table_name"],
                        ColumnName = (string)dr["column_name"],
                        Owner = (string)dr["tgt_owner"],
                        ColumnNum = (short)dr["tgt_column_num"],
                        DataType = (string)dr["tgt_data_type"],
                        Collation = dr["tgt_collation"] is DBNull ? null : (string)dr["tgt_collation"],
                        Nullable = (string)dr["tgt_nullable"],
                        DataDefault = dr["tgt_data_default"] is DBNull ? null : (string)dr["tgt_data_default"],
                        Identity = dr["tgt_identity"] is DBNull ? null : (string)dr["tgt_identity"],
                        Generated = dr["tgt_generated"] is DBNull ? null : (string)dr["tgt_generated"],
                        Storage = (string)dr["tgt_storage"],
                        Compression = dr["tgt_compression"] is DBNull ? null : (string)dr["tgt_compression"],
                        StatisticsTarget = dr["tgt_statistics_target"] is DBNull ? null : (int?)dr["tgt_statistics_target"],
                        IsLocal = (string)dr["tgt_is_local"],
                        InheritanceCount = (int)dr["tgt_inheritance_count"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        AccessPrivileges = dr["tgt_access_privileges"] is DBNull ? null : (string)dr["tgt_access_privileges"],
                        Options = dr["tgt_options"] is DBNull ? null : (string)dr["tgt_options"],
                    };
                    sourceTableColumn.Compare(targetTableColumn, this._schemaMapping, list);
                }
            }
        }

    }
}
