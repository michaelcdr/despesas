using Dapper;
using LifeManager.Infra.DBConfiguration;
using System.Data;
using System.Threading.Tasks;

namespace AndroidLanches.Infra.DBConfiguration
{
    public class CriadorBancoDeDadosMySql : ICriadorBancoDeDados
    {
        private IDbConnection _dbConnection;
        private readonly IDatabaseFactory _databaseFactory;
        public CriadorBancoDeDadosMySql(IDatabaseFactory databaseOptions)
        {
            _databaseFactory = databaseOptions;
            _dbConnection = _databaseFactory.GetDbConnection;

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
        }

        public async Task Criar()
        {
            string sqlCreateTableDespesas = @"CREATE TABLE IF NOT EXISTS  Despesas (
                                                Id  INT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
                                                DataVencimento Datetime NOT NULL, 
                                                Descritivo VARCHAR(255) NOT NULL,
                                                Parcelas INT(11) NOT NULL,
                                                Preco DECIMAL(10, 2) NOT NULL, 
                                                Titulo VARCHAR(255) NOT NULL, 
                                                Valor DECIMAL(10, 2),
                                                Pago Bit NOT NULL,
                                                DataCadastro Datetime NOT NULL
                                            );";

            await _dbConnection.ExecuteAsync(sqlCreateTableDespesas);
        }
    }
}
