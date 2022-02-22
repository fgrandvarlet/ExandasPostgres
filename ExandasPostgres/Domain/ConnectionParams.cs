using System;
using System.Text.Json.Serialization;
using Npgsql;

using ExandasPostgres.Core;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Domain
{
    public class ConnectionParams
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string DecryptedPassword
        {
            get
            {
                return CryptoUtil.DecryptIdentifier(Password, Resources.IDIOMATIC);
            }
            set
            {
                Password = CryptoUtil.EncryptIdentifier(value, Resources.IDIOMATIC);
            }
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }

        [JsonIgnore]
        public string ConnectionString
        {
            get
            {
                var csb = new NpgsqlConnectionStringBuilder
                {
                    Username = Username,
                    Password = DecryptedPassword,
                    Host = Host,
                    Port = Port,
                    Database = Database,
                    Pooling = false,

                    // syntaxe nécessaire pour initialiser le search_path à chaîne vide, pour inhiber les effet du search_path par défaut contenant : "$user", public
                    SearchPath = "''"
                };

                return csb.ConnectionString;
            }
        }

        [JsonIgnore]
        public string FormattedString
        {
            get
            {
                return string.Format(
                    "{0} [{1}@{2}:{3}/{4}]",
                    Name,
                    Username,
                    Host,
                    Port,
                    Database
                );
            }
        }

    }
}
