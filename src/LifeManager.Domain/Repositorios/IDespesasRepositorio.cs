using LifeManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeManager.Domain.Repositorios
{
    public interface IDespesasRepositorio
    {
        Task<int> Adicionar(Despesa despesa);
        Task AdicionarDespesaMensal(DespesaMensal despesaMensal);
        Task Deletar(int id);
        Task MarcarComoProcessado(int id);
        Task<List<Despesa>> ObterComRecorrencia();
        Task<List<Despesa>> ObterNaoProcessadasSemRecorrencia();
        Task<Despesa> ObterPorId(int id);
        Task<IEnumerable<Despesa>> ObterTodas();
        Task<List<DespesaMensal>> ObterTodasNaoPagas();
        Task<bool> ExisteDespesaMensalAberta(DespesaMensal despesaMensal);
        Task<decimal> ObterGastosMensal();
        Task<bool> Existe(int id);
        Task Atualizar(Despesa despesa);
    }
}
