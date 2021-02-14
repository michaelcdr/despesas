using AutoMapper;
using LifeManager.Application.Despesas.Commands;
using LifeManager.Domain.Entities;
using LifeManager.Domain.Repositorios;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LifeManager.Application.Despesas.Handlers
{
    public class CadastrarDespesaCommandHandler : IRequestHandler<CadastrarDespesaCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IDespesasRepositorio _despesasDao;

        public CadastrarDespesaCommandHandler(IMapper mapper, IDespesasRepositorio despesasDao)
        {
            _mapper = mapper;
            _despesasDao = despesasDao;
        }

        public async Task<int> Handle(CadastrarDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesa = _mapper.Map<Despesa>(request);
            int id = 0;
            if (despesa.EhValido())
                id = await _despesasDao.Adicionar(despesa);
            
            return  id;
        }
    }
}
