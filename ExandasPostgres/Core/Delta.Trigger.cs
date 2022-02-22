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
        private void DeltaTrigger(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "TRIGGER";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.trigger_name FROM src_trigger s" +
                " LEFT JOIN tgt_trigger t USING(table_name, trigger_name)" +
                " JOIN common_table USING(table_name)" +
                " WHERE t.table_name IS NULL" +
                " ORDER BY table_name, trigger_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["trigger_name"], (string)dr["table_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            //phase 2 : target minus source
            sql = "SELECT t.table_name, t.trigger_name FROM tgt_trigger t" +
                " LEFT JOIN src_trigger s USING(table_name, trigger_name)" +
                " JOIN common_table USING(table_name)" +
                " WHERE s.table_name IS NULL" +
                " ORDER BY table_name, trigger_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["trigger_name"], (string)dr["table_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_trigger";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceTrigger = new Trigger
                    {
                        TableName = (string)dr["table_name"],
                        TriggerName = (string)dr["trigger_name"],
                        Definition = (string)dr["src_definition"],
                        Enabled = (string)dr["src_enabled"],
                    };
                    var targetTrigger = new Trigger
                    {
                        TableName = (string)dr["table_name"],
                        TriggerName = (string)dr["trigger_name"],
                        Definition = (string)dr["tgt_definition"],
                        Enabled = (string)dr["tgt_enabled"],
                    };
                    sourceTrigger.Compare(targetTrigger, this._schemaMapping, list);
                }
            }
        }

    }
}
