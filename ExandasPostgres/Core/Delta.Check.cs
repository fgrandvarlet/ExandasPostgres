using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Dao;
using ExandasPostgres.Domain;

namespace ExandasPostgres.Core
{
    public partial class Delta
    {
        private void DeltaCheck(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "CHECK";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.constraint_name FROM src_check s" +
                " LEFT JOIN tgt_check t USING(table_name, constraint_name)" +
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
            sql = "SELECT t.table_name, t.constraint_name FROM tgt_check t" +
                " LEFT JOIN src_check s USING(table_name, constraint_name)" +
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
            sql = "SELECT * FROM comp_check";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceCheck = new Check
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Validated = (string)dr["src_validated"],
                        Definition = (string)dr["src_definition"],
                    };
                    var targetCheck = new Check
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Validated = (string)dr["tgt_validated"],
                        Definition = (string)dr["tgt_definition"],
                    };
                    sourceCheck.Compare(targetCheck, this._schemaMapping, list);
                }
            }
        }

    }
}
