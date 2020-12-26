using System.Threading.Tasks;

namespace LifeManager.Infra.DBConfiguration
{
    public interface ICriadorBancoDeDados
    {
        Task Criar();
    }
}
