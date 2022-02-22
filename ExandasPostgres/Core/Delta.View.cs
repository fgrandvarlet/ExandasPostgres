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
        private void DeltaView(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "VIEW";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.view_name FROM src_view s" +
                " LEFT JOIN tgt_view t USING(view_name)" +
                " WHERE t.view_name IS NULL" +
                " ORDER BY view_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["view_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.view_name FROM tgt_view t" +
                " LEFT JOIN src_view s USING(view_name)" +
                " WHERE s.view_name IS NULL" +
                " ORDER BY view_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["view_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_view";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceView = new View
                    {
                        ViewName = (string)dr["view_name"],
                        Owner = (string)dr["src_owner"],
                        Persistence = (string)dr["src_persistence"],
                        Definition = (string)dr["src_definition"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        AccessPrivileges = dr["src_access_privileges"] is DBNull ? null : (string)dr["src_access_privileges"],
                        Options = dr["src_options"] is DBNull ? null : (string)dr["src_options"],
                    };
                    var targetView = new View
                    {
                        ViewName = (string)dr["view_name"],
                        Owner = (string)dr["tgt_owner"],
                        Persistence = (string)dr["tgt_persistence"],
                        Definition = (string)dr["tgt_definition"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        AccessPrivileges = dr["tgt_access_privileges"] is DBNull ? null : (string)dr["tgt_access_privileges"],
                        Options = dr["tgt_options"] is DBNull ? null : (string)dr["tgt_options"],
                    };
                    sourceView.Compare(targetView, this._schemaMapping, list);
                }
            }
        }

    }
}
