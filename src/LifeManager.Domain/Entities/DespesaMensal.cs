using System;

namespace LifeManager.Domain.Entities
{
    public class DespesaMensal
    {
        public DespesaMensal(int id, int despesaId, bool pago, decimal valor, DateTime dataVencimento)
        {
            Id = id;
            DespesaId = despesaId;
            Pago = pago;
            Valor = valor;
            DataVencimento = dataVencimento;
        }

        public int Id { get; private set; }
        public int DespesaId { get; private set; }
        public bool Pago { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public Despesa Despesa { get; set; }
        public override string ToString()
        {
            return $"{this.DespesaId} - {this.Valor} - {this.DataVencimento.ToString("dd/MM//yyyy")}";
        }
    }
}
