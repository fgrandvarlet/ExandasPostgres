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
        private void DeltaMaterializedViewColumn(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "MATERIALIZED VIEW COLUMN";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.materialized_view_name, s.column_name FROM src_materialized_view_column s" +
                " LEFT JOIN tgt_materialized_view_column t USING(materialized_view_name, column_name)" +
                " JOIN common_materialized_view USING(materialized_view_name)" +
                " WHERE t.column_name IS NULL" +
                " ORDER BY materialized_view_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["column_name"], (string)dr["materialized_view_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.materialized_view_name, t.column_name FROM tgt_materialized_view_column t" +
                " LEFT JOIN src_materialized_view_column s USING(materialized_view_name, column_name)" +
                " JOIN common_materialized_view USING(materialized_view_name)" +
                " WHERE s.column_name IS NULL" +
                " ORDER BY materialized_view_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["column_name"], (string)dr["materialized_view_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_materialized_view_column";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceMaterializedViewColumn = new MaterializedViewColumn
                    {
                        RelationName = (string)dr["materialized_view_name"],
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
                    var targetMaterializedViewColumn = new MaterializedViewColumn
                    {
                        RelationName = (string)dr["materialized_view_name"],
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
                    sourceMaterializedViewColumn.Compare(targetMaterializedViewColumn, this._schemaMapping, list);
                }
            }
        }

    }
}
