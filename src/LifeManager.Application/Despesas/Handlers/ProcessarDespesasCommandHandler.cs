using LifeManager.Application.Despesas.Commands;
using LifeManager.Application.Despesas.Responses;
using LifeManager.Domain.Entities;
using LifeManager.Domain.Repositorios;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LifeManager.Application.Despesas.Handlers
{
    public class ProcessarDespesasCommandHandler : IRequestHandler<ProcessarDespesasCommand, ProcessarDespesasResponse>
    {
        private readonly IDespesasRepositorio _despesasDao;

        public ProcessarDespesasCommandHandler(IDespesasRepositorio despesasDao)
        {
            _despesasDao = despesasDao;
        }

        public async Task<ProcessarDespesasResponse> Handle(ProcessarDespesasCommand request, CancellationToken cancellationToken)
        {
            //sem recorrencia
            List<Despesa> despesasSemRecorrencia = await _despesasDao.ObterNaoProcessadasSemRecorrencia();

            foreach (var item in despesasSemRecorrencia)
            {
                List<DespesaMensal> despesasMensais = item.GerarDespesas();

                foreach (DespesaMensal despesaMensal in despesasMensais)
                {
                    if (!await _despesasDao.ExisteDespesaMensalAberta(despesaMensal))
                    {
                        await _despesasDao.AdicionarDespesaMensal(despesaMensal);
                        await _despesasDao.MarcarComoProcessado(item.Id);
                    }
                }
            }

            //recorrencia
            List<Despesa> despesasComRecorrencia = await _despesasDao.ObterComRecorrencia();

            foreach (var despesaComRecorrencia in despesasComRecorrencia)
            {
                List<DespesaMensal> despesasMensais = despesaComRecorrencia.GerarDespesas();

                foreach (DespesaMensal despesaMensal in despesasMensais)
                {
                    if (!await _despesasDao.ExisteDespesaMensalAberta(despesaMensal))
                    {
                        await _despesasDao.AdicionarDespesaMensal(despesaMensal);
                        await _despesasDao.MarcarComoProcessado(despesaComRecorrencia.Id);
                    }
                }
            }

            return new ProcessarDespesasResponse();
        }
    }
}