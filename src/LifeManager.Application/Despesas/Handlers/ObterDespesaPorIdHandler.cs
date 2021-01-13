using AutoMapper;
using LifeManager.Application.Despesas.Queries;
using LifeManager.Application.DTO;
using LifeManager.Domain.Entities;
using LifeManager.Domain.Repositorios;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LifeManager.Application.Despesas.Handlers
{
    public class ObterDespesaPorIdHandler : IRequestHandler<ObterDespesaPorId, DespesaDTO>
    {
        private readonly IDespesasRepositorio despesasRepositorio;
        private readonly IMapper mapper;

        public ObterDespesaPorIdHandler(IDespesasRepositorio despesasRepositorio, IMapper mapper)
        {
            this.despesasRepositorio = despesasRepositorio;
            this.mapper = mapper;
        }

        public async Task<DespesaDTO> Handle(ObterDespesaPorId request, CancellationToken cancellationToken)
        {
            Despesa despesa = await this.despesasRepositorio.ObterPorId(request.Id);

            DespesaDTO dto = this.mapper.Map<DespesaDTO>(despesa);

            return dto;
        }
    }
}
