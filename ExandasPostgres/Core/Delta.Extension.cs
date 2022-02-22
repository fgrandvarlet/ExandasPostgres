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
        private void DeltaExtension(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "EXTENSION";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.extension_name FROM src_extension s" +
                " LEFT JOIN tgt_extension t USING(extension_name)" +
                " WHERE t.extension_name IS NULL" +
                " ORDER BY extension_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["extension_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.extension_name FROM tgt_extension t" +
                " LEFT JOIN src_extension s USING(extension_name)" +
                " WHERE s.extension_name IS NULL" +
                " ORDER BY extension_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["extension_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_extension";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceExtension = new Extension
                    {
                        ExtensionName = (string)dr["extension_name"],
                        Owner = (string)dr["src_owner"],
                        Version = (string)dr["src_version"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                    };
                    var targetExtension = new Extension
                    {
                        ExtensionName = (string)dr["extension_name"],
                        Owner = (string)dr["tgt_owner"],
                        Version = (string)dr["tgt_version"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                    };
                    sourceExtension.Compare(targetExtension, this._schemaMapping, list);
                }
            }
        }

    }
}
