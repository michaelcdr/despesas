using LifeManager.Application.Despesas.Responses;
using MediatR;

namespace LifeManager.Application.Despesas.Commands
{
    public class AtualizarDespesaCommand : IRequest<AtualizarDespesaResponse>
    {
        public int Id { get; set; }
        public int DiaVencimento { get; set; }
        public int Parcelas { get; set; }
        public string Descritivo { get; set; }
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
        public bool Recorrente { get; set; }
    }
}
