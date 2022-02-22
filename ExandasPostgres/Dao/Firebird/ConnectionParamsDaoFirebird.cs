using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao.Firebird
{
    public class ConnectionParamsDaoFirebird : AbstractDaoFirebird, IConnectionParamsDao
    {
        public ConnectionParamsDaoFirebird(string connectionString) : base(connectionString)
        {
        }

        protected override FbCommand CreateCommand(Criteria criteria)
        {
            string sql;
            const string ROOT_SELECT = "SELECT uid, name, username, host, port, database" +
                " FROM connection_params" +
                " {0} ORDER BY name";

            var cmd = new FbCommand();

            if (criteria.HasText)
            {
                const string WHERE_CLAUSE = "WHERE upper(name) LIKE @pattern OR upper(username) LIKE @pattern OR upper(host) LIKE @pattern OR upper(port) LIKE @pattern" +
                    " OR upper(database) LIKE @pattern";

                sql = String.Format(ROOT_SELECT, WHERE_CLAUSE);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("pattern", criteria.Pattern.ToUpper());
            }
            else
            {
                sql = String.Format(ROOT_SELECT, string.Empty);
                cmd.CommandText = sql;
            }

            return cmd;
        }

        public ConnectionParams Get(Guid uid)
        {
            const string sql = "SELECT * FROM connection_params WHERE uid = @uid";
            ConnectionParams cp = null;

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("uid", uid);
                using (FbDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        cp = new ConnectionParams
                        {
                            Uid = Guid.Parse((string)dr["uid"]),
                            Name = (string)dr["name"],
                            Username = (string)dr["username"],
                            Password = (string)dr["password"],
                            Host = (string)dr["host"],
                            Port = (int)dr["port"],
                            Database = (string)dr["database"]
                        };
                    }
                }
            }
            return cp;
        }

        public void Add(FbTransaction tran, ConnectionParams cp)
        {
            const string sql = "INSERT INTO connection_params(uid, name, username, password, host, port, database)" +
                " VALUES(@uid, @name, @username, @password, @host, @port, @database)";

            var cmd = new FbCommand(sql, tran.Connection, tran);

            cmd.Parameters.AddWithValue("uid", cp.Uid);
            cmd.Parameters.AddWithValue("name", cp.Name);
            cmd.Parameters.AddWithValue("username", cp.Username);
            cmd.Parameters.AddWithValue("password", cp.Password);
            cmd.Parameters.AddWithValue("host", cp.Host);
            cmd.Parameters.AddWithValue("port", cp.Port);
            cmd.Parameters.AddWithValue("database", cp.Database);

            cmd.ExecuteNonQuery();
        }

        public void Add(ConnectionParams cp)
        {
            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                FbTransaction tran = conn.BeginTransaction();
                try
                {
                    Add(tran, cp);
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void Save(ConnectionParams cp)
        {
            const string sql = "UPDATE connection_params SET name = @name, username = @username, password = @password, host = @host, port = @port," +
                " database = @database WHERE uid = @uid";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);

                cmd.Parameters.AddWithValue("name", cp.Name);
                cmd.Parameters.AddWithValue("username", cp.Username);
                cmd.Parameters.AddWithValue("password", cp.Password);
                cmd.Parameters.AddWithValue("host", cp.Host);
                cmd.Parameters.AddWithValue("port", cp.Port);
                cmd.Parameters.AddWithValue("database", cp.Database);
                cmd.Parameters.AddWithValue("uid", cp.Uid);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(ConnectionParams cp)
        {
            const string sql = "DELETE FROM connection_params WHERE uid = @uid";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("uid", cp.Uid);
                cmd.ExecuteNonQuery();
            }
        }

        public List<ConnectionParams> GetList()
        {
            var list = new List<ConnectionParams>();

            const string sql = "SELECT * FROM connection_params ORDER BY name";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                using (FbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var cp = new ConnectionParams
                        {
                            Uid = Guid.Parse((string)dr["uid"]),
                            Name = (string)dr["name"],
                            Username = (string)dr["username"],
                            Password = (string)dr["password"],
                            Host = (string)dr["host"],
                            Port = (int)dr["port"],
                            Database = (string)dr["database"]
                        };

                        list.Add(cp);
                    }
                }
            }
            return list;
        }

        public int GetDependencyCount(ConnectionParams cp)
        {
            int count;

            const string sql = "SELECT count(*) FROM comparison_set WHERE connection1_uid = @uid OR connection2_uid = @uid";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("uid", cp.Uid);
                count = (int)cmd.ExecuteScalar();
            }
            return count;
        }

    }
}
