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
        private void DeltaSequence(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "SEQUENCE";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.sequence_name FROM src_sequence s" +
                " LEFT JOIN tgt_sequence t USING(sequence_name)" +
                " WHERE t.sequence_name IS NULL" +
                " ORDER BY sequence_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["sequence_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.sequence_name FROM tgt_sequence t" +
                " LEFT JOIN src_sequence s USING(sequence_name)" +
                " WHERE s.sequence_name IS NULL" +
                " ORDER BY sequence_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["sequence_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_sequence";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceSequence = new Sequence
                    {
                        SequenceName = (string)dr["sequence_name"],
                        Owner = (string)dr["src_owner"],
                        DataType = (string)dr["src_data_type"],
                        StartValue = (long)dr["src_start_value"],
                        MinValue = (long)dr["src_min_value"],
                        MaxValue = (long)dr["src_max_value"],
                        IncrementBy = (long)dr["src_increment_by"],
                        Cycle = (string)dr["src_cycle"],
                        CacheSize = (long)dr["src_cache_size"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        AccessPrivileges = dr["src_access_privileges"] is DBNull ? null : (string)dr["src_access_privileges"],
                        OwnedBy = dr["src_owned_by"] is DBNull ? null : (string)dr["src_owned_by"],
                    };
                    var targetSequence = new Sequence
                    {
                        SequenceName = (string)dr["sequence_name"],
                        Owner = (string)dr["tgt_owner"],
                        DataType = (string)dr["tgt_data_type"],
                        StartValue = (long)dr["tgt_start_value"],
                        MinValue = (long)dr["tgt_min_value"],
                        MaxValue = (long)dr["tgt_max_value"],
                        IncrementBy = (long)dr["tgt_increment_by"],
                        Cycle = (string)dr["tgt_cycle"],
                        CacheSize = (long)dr["tgt_cache_size"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        AccessPrivileges = dr["tgt_access_privileges"] is DBNull ? null : (string)dr["tgt_access_privileges"],
                        OwnedBy = dr["tgt_owned_by"] is DBNull ? null : (string)dr["tgt_owned_by"],
                    };
                    sourceSequence.Compare(targetSequence, this._schemaMapping, list);
                }
            }
        }

    }
}
