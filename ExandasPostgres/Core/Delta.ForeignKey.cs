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
        private void DeltaForeignKey(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "FOREIGN KEY";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.constraint_name FROM src_foreign_key s" +
                " LEFT JOIN tgt_foreign_key t USING(table_name, constraint_name)" +
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
            sql = "SELECT t.table_name, t.constraint_name FROM tgt_foreign_key t" +
                " LEFT JOIN src_foreign_key s USING(table_name, constraint_name)" +
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
            sql = "SELECT * FROM comp_foreign_key";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceForeignKey = new ForeignKey
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["src_deferrable"],
                        Deferred = (string)dr["src_deferred"],
                        Validated = (string)dr["src_validated"],
                        IndexName = dr["src_index_name"] is DBNull ? null : (string)dr["src_index_name"],
                        ReferencedTableName = dr["src_referenced_table_name"] is DBNull ? null : (string)dr["src_referenced_table_name"],
                        UpdateAction = (string)dr["src_update_action"],
                        DeleteAction = (string)dr["src_delete_action"],
                        MatchType = (string)dr["src_match_type"],
                        Definition = (string)dr["src_definition"],
                    };
                    var targetForeignKey = new ForeignKey
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Deferrable = (string)dr["tgt_deferrable"],
                        Deferred = (string)dr["tgt_deferred"],
                        Validated = (string)dr["tgt_validated"],
                        IndexName = dr["tgt_index_name"] is DBNull ? null : (string)dr["tgt_index_name"],
                        ReferencedTableName = dr["tgt_referenced_table_name"] is DBNull ? null : (string)dr["tgt_referenced_table_name"],
                        UpdateAction = (string)dr["tgt_update_action"],
                        DeleteAction = (string)dr["tgt_delete_action"],
                        MatchType = (string)dr["tgt_match_type"],
                        Definition = (string)dr["tgt_definition"],
                    };
                    sourceForeignKey.Compare(targetForeignKey, this._schemaMapping, list);
                }
            }
        }

    }
}
