using System;
using System.Text.Json.Serialization;

namespace LifeManager.API.DTO
{
    public class DespesaDTO
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

        public DespesaDTO()
        {
            this.DataCadastro = DateTime.Now;
        }
    }
}
