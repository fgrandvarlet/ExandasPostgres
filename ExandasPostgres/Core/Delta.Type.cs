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
        private void DeltaType(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "TYPE";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.type_name FROM src_type s" +
                " LEFT JOIN tgt_type t USING(type_name)" +
                " WHERE t.type_name IS NULL" +
                " ORDER BY type_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["type_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.type_name FROM tgt_type t" +
                " LEFT JOIN src_type s USING(type_name)" +
                " WHERE s.type_name IS NULL" +
                " ORDER BY type_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["type_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_type";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceType = new PostgresType
                    {
                        TypeName = (string)dr["type_name"],
                        FormatTypeName = (string)dr["src_format_type_name"],
                        Owner = (string)dr["src_owner"],
                        TypeType = dr["src_type_type"] is DBNull ? null : (string)dr["src_type_type"],
                        TypeSize = (string)dr["src_type_size"],
                        Elements = dr["src_elements"] is DBNull ? null : (string)dr["src_elements"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        AccessPrivileges = dr["src_access_privileges"] is DBNull ? null : (string)dr["src_access_privileges"],
                    };
                    var targetType = new PostgresType
                    {
                        TypeName = (string)dr["type_name"],
                        FormatTypeName = (string)dr["tgt_format_type_name"],
                        Owner = (string)dr["tgt_owner"],
                        TypeType = dr["tgt_type_type"] is DBNull ? null : (string)dr["tgt_type_type"],
                        TypeSize = (string)dr["tgt_type_size"],
                        Elements = dr["tgt_elements"] is DBNull ? null : (string)dr["tgt_elements"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        AccessPrivileges = dr["tgt_access_privileges"] is DBNull ? null : (string)dr["tgt_access_privileges"],
                    };
                    sourceType.Compare(targetType, this._schemaMapping, list);
                }
            }
        }

    }
}
