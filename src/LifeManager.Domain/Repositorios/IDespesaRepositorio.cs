using LifeManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeManager.Domain.Repositorios
{
    public interface IDespesaRepositorio : IDisposable
    {
        Task<int> Adicionar(Despesa despesa);
        Task<Despesa> ObterPorId(int id);
        Task<List<Despesa>> ObterTodasNaoPagas();
        Task<IEnumerable<Despesa>> ObterTodas();
        Task Deletar(int id);
        Task<List<Despesa>> ObterNaoProcessadasSemRecorrencia();
        Task AdicionarDespesaMensal(DespesaMensal despesaMensal);
        Task<List<Despesa>> ObterComRecorrencia();
        Task MarcarComoProcessado(int id);
    }
}
