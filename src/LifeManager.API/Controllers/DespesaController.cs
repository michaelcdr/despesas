using LifeManager.Application.Despesas.Commands;
using LifeManager.Domain.Entities;
using LifeManager.Domain.Repositorios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeManager.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("v1/despesa")]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesasRepositorio _despesasDao;
        private readonly IMediator _mediator;
        public DespesaController(IDespesasRepositorio despesas, IMediator mediator)
        {
            this._despesasDao = despesas;
            this._mediator = mediator;
        }

        /// <summary>
        /// Obtem uma lista de despesas disponiveis.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Despesa>> Get()
            => await _despesasDao.ObterTodas();
        
        /// <summary>
        /// Obter dados de uma Despesa pelo ID
        /// </summary>
        /// <param name="id">Id da despesa</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _despesasDao.ObterPorId(id));
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastrarDespesaCommand command)
        {
            int despesaId = await _mediator.Send(command);
            return CreatedAtRoute("", despesaId);
        }

        [HttpPost("ProcessarDespesas")]
        public async Task<IActionResult> ProcessarDespesas()
        {
            await _mediator.Send(new ProcessarDespesasCommand());
           
            return Ok();
        }

        [HttpGet("ObterTotalDespesasMensal")]
        public async Task<IActionResult> ObterTotalDespesasMensal()
            => Ok(await _despesasDao.ObterGastosMensal());

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _despesasDao.Existe(id)) return NotFound();

            await _despesasDao.Deletar(id);
            return NoContent();
        }
    }
}