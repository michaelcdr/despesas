using LifeManager.Infra.DBConfiguration;
using System.Data;

namespace LifeManager.Infra.Repositories
{
    public abstract class BaseRepository 
    {
        protected IDbConnection _dbConnection;
        protected readonly IDatabaseFactory _databaseFactory;
       
        protected BaseRepository(IDatabaseFactory databaseOptions)
        {
            _databaseFactory = databaseOptions;
            _dbConnection = _databaseFactory.GetDbConnection;
        }

        private IDbConnection ObterConexao()
        {
            _dbConnection = _databaseFactory.GetDbConnection;
            return _dbConnection;
        }

        public IDbConnection Conexao
        {
            get
            {
                return ObterConexao();
            }
        }
    }
}
