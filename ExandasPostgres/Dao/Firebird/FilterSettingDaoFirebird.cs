﻿using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao.Firebird
{
    public class FilterSettingDaoFirebird : AbstractDaoFirebird, IFilterSettingDao
    {
        public FilterSettingDaoFirebird(string connectionString) : base(connectionString)
        {
        }

        protected override FbCommand CreateCommand(Criteria criteria)
        {
            var comparisonSet = (ComparisonSet)criteria.Entity;

            string sql;
            const string ROOT_SELECT = "SELECT id, entity, label, property" +
                " FROM filter_setting WHERE comparison_set_uid = @comparison_set_uid" +
                " {0} ORDER BY id";

            var cmd = new FbCommand();

            if (criteria.HasText)
            {
                const string WHERE_CLAUSE = "AND (upper(entity) LIKE @pattern OR upper(label) LIKE @pattern OR upper(property) LIKE @pattern)";

                sql = String.Format(ROOT_SELECT, WHERE_CLAUSE);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSet.Uid);
                cmd.Parameters.AddWithValue("pattern", criteria.Pattern.ToUpper());
            }
            else
            {
                sql = String.Format(ROOT_SELECT, string.Empty);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSet.Uid);
            }

            return cmd;
        }

        public FilterSetting Get(int id)
        {
            const string sql = "SELECT * FROM filter_setting WHERE id = @id";
            FilterSetting fs = null;

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (FbDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        fs = new FilterSetting
                        {
                            Id = (int)dr["id"],
                            ComparisonSetUid = Guid.Parse((string)dr["comparison_set_uid"]),
                            Entity = (string)dr["entity"],
                            LabelId = dr["label_id"] is DBNull ? null : (short?)dr["label_id"],
                            Label = dr["label"] is DBNull ? null : (string)dr["label"],
                            Property = dr["property"] is DBNull ? null : (string)dr["property"]
                        };
                    }
                }
            }
            return fs;
        }

        public void Add(FbTransaction tran, FilterSetting fs)
        {
            const string sql = "INSERT INTO filter_setting(comparison_set_uid, entity, label_id, label, property)" +
                " VALUES(@comparison_set_uid, @entity, @label_id, @label, @property)";

            var cmd = new FbCommand(sql, tran.Connection, tran);

            cmd.Parameters.AddWithValue("comparison_set_uid", fs.ComparisonSetUid);
            cmd.Parameters.AddWithValue("entity", fs.Entity);
            cmd.Parameters.AddWithValue("label_id", fs.LabelId);
            cmd.Parameters.AddWithValue("label", fs.Label);
            cmd.Parameters.AddWithValue("property", fs.Property);

            cmd.ExecuteNonQuery();
        }

        public void Add(FilterSetting fs)
        {
            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                FbTransaction tran = conn.BeginTransaction();
                try
                {
                    Add(tran, fs);
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void Delete(FilterSetting fs)
        {
            const string sql = "DELETE FROM filter_setting WHERE id = @id";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("id", fs.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public string GetFilteringWhereClause(Guid comparisonSetUid)
        {
            var statements = new List<string>();

            foreach (FilterSetting fs in GetListByComparisonSetUid(comparisonSetUid))
            {
                statements.Add(fs.Predicate);
            }

            if (statements.Count > 0)
            {
                return string.Format(" AND ({0})", string.Join(" AND ", statements));
            }
            else
            {
                return string.Empty;
            }
        }

        public List<FilterSetting> GetListByComparisonSetUid(Guid comparisonSetUid)
        {
            var list = new List<FilterSetting>();

            const string sql = "SELECT * FROM filter_setting WHERE comparison_set_uid = @comparison_set_uid ORDER BY id";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSetUid);

                using (FbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var fs = new FilterSetting
                        {
                            Id = (int)dr["id"],
                            ComparisonSetUid = Guid.Parse((string)dr["comparison_set_uid"]),
                            Entity = (string)dr["entity"],
                            LabelId = dr["label_id"] is DBNull ? null : (short?)dr["label_id"],
                            Label = dr["label"] is DBNull ? null : (string)dr["label"],
                            Property = dr["property"] is DBNull ? null : (string)dr["property"]
                        };

                        list.Add(fs);
                    }
                }
            }
            return list;
        }

    }
}
