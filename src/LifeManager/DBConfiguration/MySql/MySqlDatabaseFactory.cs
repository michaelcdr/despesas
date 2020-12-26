using LifeManager.Infra.DBConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace AndroidLanches.Infra.DBConfiguration
{
    public class MySqlDatabaseFactory : IDatabaseFactory
    {
        private IOptions<DataSettings> dataSettings;

        protected string ConnectionString => !string.IsNullOrEmpty(dataSettings.Value.DefaultConnection) 
            ? dataSettings.Value.DefaultConnection 
            : DatabaseConnection.ConnectionConfiguration.GetConnectionString("DefaultConnection");

        public IDbConnection GetDbConnection => new MySqlConnection(ConnectionString);

        public MySqlDatabaseFactory(IOptions<DataSettings> dataSettings)
        {
            this.dataSettings = dataSettings;
        }
    }
}