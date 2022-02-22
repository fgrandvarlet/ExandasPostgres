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
        private void DeltaDomain(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "DOMAIN";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.domain_name FROM src_domain s" +
                " LEFT JOIN tgt_domain t USING(domain_name)" +
                " WHERE t.domain_name IS NULL" +
                " ORDER BY domain_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["domain_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.domain_name FROM tgt_domain t" +
                " LEFT JOIN src_domain s USING(domain_name)" +
                " WHERE s.domain_name IS NULL" +
                " ORDER BY domain_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["domain_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_domain";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceDomain = new Domain.Domain
                    {
                        DomainName = (string)dr["domain_name"],
                        Owner = (string)dr["src_owner"],
                        DataType = (string)dr["src_data_type"],
                        Collation = dr["src_collation"] is DBNull ? null : (string)dr["src_collation"],
                        Nullable = (string)dr["src_nullable"],
                        DataDefault = dr["src_data_default"] is DBNull ? null : (string)dr["src_data_default"],
                        CheckConstraint = dr["src_check_constraint"] is DBNull ? null : (string)dr["src_check_constraint"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        AccessPrivileges = dr["src_access_privileges"] is DBNull ? null : (string)dr["src_access_privileges"],
                    };
                    var targetDomain = new Domain.Domain
                    {
                        DomainName = (string)dr["domain_name"],
                        Owner = (string)dr["tgt_owner"],
                        DataType = (string)dr["tgt_data_type"],
                        Collation = dr["tgt_collation"] is DBNull ? null : (string)dr["tgt_collation"],
                        Nullable = (string)dr["tgt_nullable"],
                        DataDefault = dr["tgt_data_default"] is DBNull ? null : (string)dr["tgt_data_default"],
                        CheckConstraint = dr["tgt_check_constraint"] is DBNull ? null : (string)dr["tgt_check_constraint"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        AccessPrivileges = dr["tgt_access_privileges"] is DBNull ? null : (string)dr["tgt_access_privileges"],
                    };
                    sourceDomain.Compare(targetDomain, this._schemaMapping, list);
                }
            }
        }

    }
}
