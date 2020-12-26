using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace LifeManager.Infra.DBConfiguration
{
    public class SqlServerDatabaseFactory : IDatabaseFactory
    {
        private IOptions<DataSettings> _dataSettings;

        protected string ConnectionString => !string.IsNullOrEmpty(_dataSettings.Value.DefaultConnection) 
            ? _dataSettings.Value.DefaultConnection 
            : DatabaseConnection.ConnectionConfiguration.GetConnectionString("DefaultConnection");

        public IDbConnection GetDbConnection => new SqlConnection(ConnectionString);

        public SqlServerDatabaseFactory(IOptions<DataSettings> dataSettings)
        {
            this._dataSettings = dataSettings;
        }
    }
}