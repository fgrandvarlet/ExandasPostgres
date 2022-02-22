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
        private void DeltaMaterializedView(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "MATERIALIZED VIEW";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.materialized_view_name FROM src_materialized_view s" +
                " LEFT JOIN tgt_materialized_view t USING(materialized_view_name)" +
                " WHERE t.materialized_view_name IS NULL" +
                " ORDER BY materialized_view_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["materialized_view_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.materialized_view_name FROM tgt_materialized_view t" +
                " LEFT JOIN src_materialized_view s USING(materialized_view_name)" +
                " WHERE s.materialized_view_name IS NULL" +
                " ORDER BY materialized_view_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["materialized_view_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_materialized_view";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceMaterializedView = new MaterializedView
                    {
                        MaterializedViewName = (string)dr["materialized_view_name"],
                        Owner = (string)dr["src_owner"],
                        TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"],
                        Persistence = (string)dr["src_persistence"],
                        AccessMethod = dr["src_access_method"] is DBNull ? null : (string)dr["src_access_method"],
                        Definition = (string)dr["src_definition"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        AccessPrivileges = dr["src_access_privileges"] is DBNull ? null : (string)dr["src_access_privileges"],
                        Options = dr["src_options"] is DBNull ? null : (string)dr["src_options"],
                    };
                    var targetMaterializedView = new MaterializedView
                    {
                        MaterializedViewName = (string)dr["materialized_view_name"],
                        Owner = (string)dr["tgt_owner"],
                        TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"],
                        Persistence = (string)dr["tgt_persistence"],
                        AccessMethod = dr["tgt_access_method"] is DBNull ? null : (string)dr["tgt_access_method"],
                        Definition = (string)dr["tgt_definition"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        AccessPrivileges = dr["tgt_access_privileges"] is DBNull ? null : (string)dr["tgt_access_privileges"],
                        Options = dr["tgt_options"] is DBNull ? null : (string)dr["tgt_options"],
                    };
                    sourceMaterializedView.Compare(targetMaterializedView, this._schemaMapping, list);
                }
            }
        }

    }
}
