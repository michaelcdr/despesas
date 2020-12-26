using System.Data;

namespace LifeManager.Infra.DBConfiguration
{
    public interface IDatabaseFactory
    {
        IDbConnection GetDbConnection { get; }
    }
}
