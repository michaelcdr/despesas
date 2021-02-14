using AutoMapper;
using LifeManager.Application.Despesas.Commands;
using LifeManager.Application.DTO;
using LifeManager.Domain.Entities;

namespace LifeManager.API.Profiles
{
    public class DespesaMap : Profile
    {
        public DespesaMap()
        {
            CreateMap<DespesaDTO, Despesa>();
            CreateMap<Despesa, DespesaDTO>();
            CreateMap<CadastrarDespesaCommand, Despesa>();
            CreateMap<AtualizarDespesaCommand, Despesa>();
        }
    }
}
