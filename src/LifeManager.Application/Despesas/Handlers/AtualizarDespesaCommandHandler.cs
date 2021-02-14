using AutoMapper;
using LifeManager.Application.Despesas.Commands;
using LifeManager.Application.Despesas.Responses;
using LifeManager.Domain.Entities;
using LifeManager.Domain.Repositorios;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LifeManager.Application.Despesas.Handlers
{
    public class AtualizarDespesaCommandHandler : IRequestHandler<AtualizarDespesaCommand, AtualizarDespesaResponse>
    {
        private readonly IDespesasRepositorio _despesasRepositorio;
        private readonly IMapper _mapper;

        public AtualizarDespesaCommandHandler(IDespesasRepositorio despesasRepositorio, IMapper mapper)
        {
            this._despesasRepositorio = despesasRepositorio;
            this._mapper = mapper;
        }

        public async Task<AtualizarDespesaResponse> Handle(AtualizarDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesa = _mapper.Map<Despesa>(request);
            if (despesa.EhValido())
            {
                await _despesasRepositorio.Atualizar(despesa);

                return new AtualizarDespesaResponse("Despesa atualizada com sucesso", true);
            } 
            else
                return new AtualizarDespesaResponse("Não foi possivel atualizar a despesa.", false);
        }
    }
}
