using LifeManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeManager.Domain.Repositorios
{
    public interface ILembretesRepositorio
    {
        Task<List<Lembrete>> ObterTodos();


    }
}
