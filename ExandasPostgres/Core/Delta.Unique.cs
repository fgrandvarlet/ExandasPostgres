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
        private void DeltaUnique(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "UNIQUE";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.constraint_name FROM src_unique s" +
                " LEFT JOIN tgt_unique t USING(table_name, constraint_name)" +
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
            sql = "SELECT t.table_name, t.constraint_name FROM tgt_unique t" +
                " LEFT JOIN src_unique s USING(table_name, constraint_name)" +
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
            sql = "SELECT * FROM comp_unique";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceUnique = new Unique
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["src_deferrable"],
                        Deferred = (string)dr["src_deferred"],
                        Definition = (string)dr["src_definition"],
                    };
                    var targetUnique = new Unique
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["tgt_deferrable"],
                        Deferred = (string)dr["tgt_deferred"],
                        Definition = (string)dr["tgt_definition"],
                    };
                    sourceUnique.Compare(targetUnique, this._schemaMapping, list);
                }
            }
        }

    }
}
