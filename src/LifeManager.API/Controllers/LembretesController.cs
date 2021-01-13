using AutoMapper;
using LifeManager.Application.DTO;
using LifeManager.Domain.Entities;
using LifeManager.Domain.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeManager.API.Controllers
{
    [ApiController]
    [Route("v1/lembrete")]
    public class LembretesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILembretesRepositorio _lembretesDao;

        public LembretesController(IMapper mapper, ILembretesRepositorio lembretesDAO)
        {
            this._mapper = mapper;
            this._lembretesDao = lembretesDAO;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Lembrete> lembretes = await _lembretesDao.ObterTodos();
            List<LembreteDTO> lembretesDtos = new List<LembreteDTO>();

            if (lembretes != null && lembretes.Count > 0)
                foreach (Lembrete lembrete in lembretes)
                    lembretesDtos.Add(_mapper.Map<LembreteDTO>(lembrete));

            return Ok(lembretes);
        }
    }
}