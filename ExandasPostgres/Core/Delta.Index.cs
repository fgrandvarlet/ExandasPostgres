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
        private void DeltaIndex(FbConnection conn, List<DeltaReport> list)
        {
            const string ENTITY = "INDEX";
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.index_name, s.table_name FROM src_index s" +
                " LEFT JOIN tgt_index t USING(index_name, table_name)" +
                " JOIN common_table_index USING(table_name)" +
                " WHERE t.index_name IS NULL" +
                " ORDER BY table_name, index_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["index_name"], (string)dr["table_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.index_name, t.table_name FROM tgt_index t" +
                " LEFT JOIN src_index s USING(index_name, table_name)" +
                " JOIN common_table_index USING(table_name)" +
                " WHERE s.index_name IS NULL" +
                " ORDER BY table_name, index_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._schemaMapping.Uid, ENTITY, (string)dr["index_name"], (string)dr["table_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_index";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceIndex = new Index
                    {
                        IndexName = (string)dr["index_name"],
                        TableName = (string)dr["table_name"],
                        Owner = (string)dr["src_owner"],
                        TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"],
                        Persistence = (string)dr["src_persistence"],
                        AccessMethod = dr["src_access_method"] is DBNull ? null : (string)dr["src_access_method"],
                        IsPartitioned = (string)dr["src_is_partitioned"],
                        IsPartition = (string)dr["src_is_partition"],
                        HasSubclass = (string)dr["src_has_subclass"],
                        IsUnique = (string)dr["src_is_unique"],
                        IsPrimary = (string)dr["src_is_primary"],
                        IsExclusion = (string)dr["src_is_exclusion"],
                        Immediate = (string)dr["src_immediate"],
                        IsClustered = (string)dr["src_is_clustered"],
                        IsValid = (string)dr["src_is_valid"],
                        Definition = (string)dr["src_definition"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        Options = dr["src_options"] is DBNull ? null : (string)dr["src_options"],
                    };
                    var targetIndex = new Index
                    {
                        IndexName = (string)dr["index_name"],
                        TableName = (string)dr["table_name"],
                        Owner = (string)dr["tgt_owner"],
                        TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"],
                        Persistence = (string)dr["tgt_persistence"],
                        AccessMethod = dr["tgt_access_method"] is DBNull ? null : (string)dr["tgt_access_method"],
                        IsPartitioned = (string)dr["tgt_is_partitioned"],
                        IsPartition = (string)dr["tgt_is_partition"],
                        HasSubclass = (string)dr["tgt_has_subclass"],
                        IsUnique = (string)dr["tgt_is_unique"],
                        IsPrimary = (string)dr["tgt_is_primary"],
                        IsExclusion = (string)dr["tgt_is_exclusion"],
                        Immediate = (string)dr["tgt_immediate"],
                        IsClustered = (string)dr["tgt_is_clustered"],
                        IsValid = (string)dr["tgt_is_valid"],
                        Definition = (string)dr["tgt_definition"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        Options = dr["tgt_options"] is DBNull ? null : (string)dr["tgt_options"],
                    };
                    sourceIndex.Compare(targetIndex, this._schemaMapping, list);
                }
            }
        }

    }
}
