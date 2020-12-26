using MediatR;
using System;
using System.Text.Json.Serialization;

namespace LifeManager.Application.Despesas.Commands
{
    public class CadastrarDespesaCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int DiaVencimento { get; set; }
        public int Parcelas { get; set; }
        public string Descritivo { get; set; }
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
        public bool Recorrente { get; set; }
        public DateTime DataInicio { get; set; }

        [JsonIgnore]
        public DateTime DataCadastro { get; set; }

        public CadastrarDespesaCommand()
        {
            this.DataCadastro = DateTime.Now;
        }
    }
}
