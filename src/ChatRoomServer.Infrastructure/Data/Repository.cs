using System.Data.Common;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ChatRoomServer.Infrastructure.Data
{
    internal abstract class Repository
    {
        private readonly IConfiguration configuration;

        public Repository(IConfiguration config)
        {
            this.configuration = config;
        }

        protected DbConnection CreateConnection()
        {
            var connBuilder = new MySqlConnectionStringBuilder();
            connBuilder.Add("Server", configuration["DB_HOST"]);
            connBuilder.Add("Database", configuration["DB_NAME"]);
            connBuilder.Add("Uid", configuration["DB_USER"]);
            connBuilder.Add("Pwd", configuration["DB_PASSWORD"]);

            return new MySqlConnection(connBuilder.ConnectionString);
        }

    }
}