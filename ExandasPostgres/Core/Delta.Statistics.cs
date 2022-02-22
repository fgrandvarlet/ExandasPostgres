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
        private void DeltaStatistics(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "STATISTICS";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.statistics_name FROM src_statistics s" +
                " LEFT JOIN tgt_statistics t USING(statistics_name)" +
                " WHERE t.statistics_name IS NULL" +
                " ORDER BY statistics_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["statistics_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.statistics_name FROM tgt_statistics t" +
                " LEFT JOIN src_statistics s USING(statistics_name)" +
                " WHERE s.statistics_name IS NULL" +
                " ORDER BY statistics_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["statistics_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_statistics";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceStatistics = new Statistics
                    {
                        StatisticsName = (string)dr["statistics_name"],
                        Owner = (string)dr["src_owner"],
                        Definition = (string)dr["src_definition"],
                        NDistinct = dr["src_ndistinct"] is DBNull ? null : (string)dr["src_ndistinct"],
                        Dependencies = dr["src_dependencies"] is DBNull ? null : (string)dr["src_dependencies"],
                        MCV = dr["src_mcv"] is DBNull ? null : (string)dr["src_mcv"],
                        StatisticsTarget = (int)dr["src_statistics_target"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                    };
                    var targetStatistics = new Statistics
                    {
                        StatisticsName = (string)dr["statistics_name"],
                        Owner = (string)dr["tgt_owner"],
                        Definition = (string)dr["tgt_definition"],
                        NDistinct = dr["tgt_ndistinct"] is DBNull ? null : (string)dr["tgt_ndistinct"],
                        Dependencies = dr["tgt_dependencies"] is DBNull ? null : (string)dr["tgt_dependencies"],
                        MCV = dr["tgt_mcv"] is DBNull ? null : (string)dr["tgt_mcv"],
                        StatisticsTarget = (int)dr["tgt_statistics_target"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                    };
                    sourceStatistics.Compare(targetStatistics, this._schemaMapping, list);
                }
            }
        }

    }
}
