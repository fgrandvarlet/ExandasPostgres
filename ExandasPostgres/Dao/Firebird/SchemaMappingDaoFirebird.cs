using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao.Firebird
{
    public class SchemaMappingDaoFirebird : AbstractDaoFirebird, ISchemaMappingDao
    {
        public SchemaMappingDaoFirebird(string connectionString) : base(connectionString)
        {
        }

        protected override FbCommand CreateCommand(Criteria criteria)
        {
            var comparisonSet = (ComparisonSet)criteria.Entity;

            string sql;
            const string ROOT_SELECT = "SELECT uid, schema1, schema2" +
                " FROM schema_mapping WHERE comparison_set_uid = @comparison_set_uid" +
                " {0} ORDER BY no_order";

            var cmd = new FbCommand();

            if (criteria.HasText)
            {
                const string AND_CLAUSE = "AND (upper(schema1) LIKE @pattern OR upper(schema2) LIKE @pattern)";

                sql = String.Format(ROOT_SELECT, AND_CLAUSE);
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

        public SchemaMapping Get(Guid uid)
        {
            const string sql = "SELECT * FROM schema_mapping WHERE uid = @uid";
            SchemaMapping sm = null;

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("uid", uid);
                using (FbDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        sm = new SchemaMapping
                        {
                            Uid = Guid.Parse((string)dr["uid"]),
                            ComparisonSetUid = Guid.Parse((string)dr["comparison_set_uid"]),
                            Schema1 = (string)dr["schema1"],
                            Schema2 = (string)dr["schema2"],
                            NoOrder = (int)dr["no_order"]
                        };
                    }
                }
            }
            return sm;
        }

        public void Add(FbTransaction tran, SchemaMapping sm)
        {
            const string sql = "INSERT INTO schema_mapping(uid, comparison_set_uid, schema1, schema2, no_order)" +
                " VALUES(@uid, @comparison_set_uid, @schema1, @schema2, @no_order)";

            var cmd = new FbCommand(sql, tran.Connection, tran);

            cmd.Parameters.AddWithValue("uid", sm.Uid);
            cmd.Parameters.AddWithValue("comparison_set_uid", sm.ComparisonSetUid);
            cmd.Parameters.AddWithValue("schema1", sm.Schema1);
            cmd.Parameters.AddWithValue("schema2", sm.Schema2);
            cmd.Parameters.AddWithValue("no_order", sm.NoOrder);

            cmd.ExecuteNonQuery();
        }

        public void Add(SchemaMapping sm)
        {
            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                FbTransaction tran = conn.BeginTransaction();
                try
                {
                    if (sm.NoOrder == 0)
                    {
                        const string sql = "SELECT coalesce(max(no_order), 0) FROM schema_mapping WHERE comparison_set_uid = @comparison_set_uid";
                        var cmd = new FbCommand(sql, conn, tran);
                        cmd.Parameters.AddWithValue("comparison_set_uid", sm.ComparisonSetUid);

                        int result = (int)cmd.ExecuteScalar();
                        sm.NoOrder = result + 1;
                    }

                    Add(tran, sm);
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void Save(SchemaMapping sm)
        {
            const string sql = "UPDATE schema_mapping SET schema1 = @schema1, schema2 = @schema2, no_order = @no_order WHERE uid = @uid";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);

                cmd.Parameters.AddWithValue("schema1", sm.Schema1);
                cmd.Parameters.AddWithValue("schema2", sm.Schema2);
                cmd.Parameters.AddWithValue("no_order", sm.NoOrder);
                cmd.Parameters.AddWithValue("uid", sm.Uid);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(SchemaMapping sm)
        {
            const string sql = "DELETE FROM schema_mapping WHERE uid = @uid";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("uid", sm.Uid);
                cmd.ExecuteNonQuery();
            }
        }

        public List<SchemaMapping> GetListByComparisonSetUid(Guid comparisonSetUid)
        {
            var list = new List<SchemaMapping>();

            const string sql = "SELECT * FROM schema_mapping WHERE comparison_set_uid = @comparison_set_uid ORDER BY no_order";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSetUid);

                using (FbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var sm = new SchemaMapping
                        {
                            Uid = Guid.Parse((string)dr["uid"]),
                            ComparisonSetUid = Guid.Parse((string)dr["comparison_set_uid"]),
                            Schema1 = (string)dr["schema1"],
                            Schema2 = (string)dr["schema2"],
                            NoOrder = (int)dr["no_order"]
                        };

                        list.Add(sm);
                    }
                }
            }
            return list;
        }

    }
}
