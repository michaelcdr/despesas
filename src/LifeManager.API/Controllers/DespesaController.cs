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
        private readonly IDespesasRepositorio _despesasRepository;
        private readonly IMediator _mediator;
        public DespesaController(IDespesasRepositorio despesas, IMediator mediator)
        {
            this._despesasRepository = despesas;
            this._mediator = mediator;
        }

        /// <summary>
        /// Obtem uma lista de despesas disponiveis.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Despesa>> Get()
            => await _despesasRepository.ObterTodas();
        
        /// <summary>
        /// Obter dados de uma Despesa pelo ID
        /// </summary>
        /// <param name="id">Id da despesa</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _despesasRepository.ObterPorId(id));
        
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
            => Ok(await _despesasRepository.ObterGastosMensal());

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _despesasRepository.Existe(id)) return NotFound();

            await _despesasRepository.Deletar(id);
            return NoContent();
        }
    }
}