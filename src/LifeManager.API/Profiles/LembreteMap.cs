using AutoMapper;
using LifeManager.Application.DTO;
using LifeManager.Domain.Entities;
using System.Linq;

namespace LifeManager.API.Profiles
{
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
