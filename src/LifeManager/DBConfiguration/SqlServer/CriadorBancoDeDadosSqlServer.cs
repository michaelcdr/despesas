using Dapper;
using LifeManager.Infra.DBConfiguration;
using System.Data;
using System.Threading.Tasks;

namespace AndroidLanches.Infra.DBConfiguration
{
    public class CriadorBancoDeDadosSqlServer: ICriadorBancoDeDados
    {
        private IDbConnection _dbConnection;
        private readonly IDatabaseFactory _databaseFactory;
        public CriadorBancoDeDadosSqlServer(IDatabaseFactory databaseOptions)
        {
            _databaseFactory = databaseOptions;
            _dbConnection = _databaseFactory.GetDbConnection;

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
        }

        public async Task Criar()
        {
            string sqlCreateTableDespesas = @"IF	NOT EXISTS (SELECT * FROM sysobjects
            WHERE id = object_id(N'[dbo].[Despesas]')
	            AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
            CREATE TABLE [dbo].Despesas ( 
	            Id INT  NOT NULL IDENTITY PRIMARY KEY, 
	            DiaVencimento INT NOT NULL, 
                Descritivo VARCHAR(255) NOT NULL,
                Parcelas INT NOT NULL,                
                Titulo VARCHAR(255) NOT NULL, 
                Valor DECIMAL(10, 2),
                Pago Bit NOT NULL,
                DataCadastro Datetime NOT NULL
            );";

            await _dbConnection.ExecuteAsync(sqlCreateTableDespesas);
        }
    }
}
