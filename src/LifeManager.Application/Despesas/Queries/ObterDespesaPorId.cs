using LifeManager.Application.DTO;
using MediatR;

namespace LifeManager.Application.Despesas.Queries
{
    public  class ObterDespesaPorId:IRequest<DespesaDTO>
    {
        public ObterDespesaPorId(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
