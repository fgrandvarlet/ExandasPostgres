using Npgsql;

namespace ExandasPostgres.Dao.PostgreSQL
{
    public abstract class AbstractDaoPostgreSQL
    {
        protected readonly string _connectionString;

        protected AbstractDaoPostgreSQL(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public NpgsqlConnection GetNpgsqlConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

    }
}
