using AutoMapper;
using LifeManager.API.DTO;
using LifeManager.Application.Despesas.Commands;
using LifeManager.Domain.Entities;
using System.Linq;

namespace LifeManager.API.Profiles
{
    public class DespesaMap : Profile
    {
        public DespesaMap()
        {
            CreateMap<DespesaDTO, Despesa>();
            CreateMap<CadastrarDespesaCommand, Despesa>();
        }
    }

    public class LembreteMap : Profile
    {
        public LembreteMap()
        {
            CreateMap<Lembrete, LembreteDTO>()
                .ForMember(dest => dest.Locais, opt => 
                    opt.MapFrom(src => src.Locais.Select(local => new LocaisDTO 
                    { 
                        Id = local.Id,
                        Descritivo = local.Descritivo, 
                        Latitude = local.Latitude,
                        Longitude = local.Longitude
                    })));
        }
    }
}
