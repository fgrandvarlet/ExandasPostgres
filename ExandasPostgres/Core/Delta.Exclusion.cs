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
        private void DeltaExclusion(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "EXCLUSION";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.constraint_name FROM src_exclusion s" +
                " LEFT JOIN tgt_exclusion t USING(table_name, constraint_name)" +
                " JOIN common_table USING(table_name)" +
                " WHERE t.table_name IS NULL" +
                " ORDER BY table_name, constraint_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["constraint_name"], (string)dr["table_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name, t.constraint_name FROM tgt_exclusion t" +
                " LEFT JOIN src_exclusion s USING(table_name, constraint_name)" +
                " JOIN common_table USING(table_name)" +
                " WHERE s.table_name IS NULL" +
                " ORDER BY table_name, constraint_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["constraint_name"], (string)dr["table_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_exclusion";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceExclusion = new Exclusion
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["src_deferrable"],
                        Deferred = (string)dr["src_deferred"],
                        Definition = (string)dr["src_definition"],
                    };
                    var targetExclusion = new Exclusion
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["tgt_deferrable"],
                        Deferred = (string)dr["tgt_deferred"],
                        Definition = (string)dr["tgt_definition"],
                    };
                    sourceExclusion.Compare(targetExclusion, this._schemaMapping, list);
                }
            }
        }

    }
}
