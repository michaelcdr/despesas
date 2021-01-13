using System;
using System.Collections.Generic;

namespace LifeManager.Domain.Entities
{
    public class Despesa
    {
        public Despesa()
        {
            DespesasMensais = new List<DespesaMensal>();
        }

        public Despesa(int id, int diaVencimento, int parcelas, string descritivo, string titulo, decimal valor, bool processado, DateTime dataInicio)
        {
            Id = id;
            DiaVencimento = diaVencimento;
            Parcelas = parcelas;
            Descritivo = descritivo;
            Titulo = titulo;
            Valor = valor;
            Processado = processado;
            DataCadastro = DateTime.Now;
            DataInicio = dataInicio;
            DespesasMensais = new List<DespesaMensal>();
        }

        public int Id { get;  private set; }
        public int DiaVencimento { get; private set; }
        public int Parcelas { get; private set; }
        public string Descritivo { get; private set; }
        public string Titulo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataInicio { get; private set; }
        public bool Processado { get; private set; }
        public bool Recorrente { get; private set; }
        public List<DespesaMensal> DespesasMensais { get; private set;  }

        /// <summary>
        /// A partir do metodo de parcelas, iremos decompor a despesa em um numero de despesas mensais.
        /// </summary>
        /// <returns>Retorna uma lista das despesas referente a cada parcela</returns>
        public List<DespesaMensal> GerarDespesas()
        {
            var despesasMensais = new List<DespesaMensal>();
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            const int DEZEMBRO = 12;
            for (int indice = 0; indice < this.Parcelas; indice++)
            {
                DateTime dataVencimento = new DateTime(ano, mes, this.DiaVencimento);
                if (mes == DEZEMBRO)
                {
                    mes = 1;
                    ano++;
                }
                else
                    mes++;
                
                decimal valorParcela = CalcularParcela();
                despesasMensais.Add(new DespesaMensal(0, this.Id, false, valorParcela, dataVencimento));
            }
            return despesasMensais;
        }

        private decimal CalcularParcela()
            => Math.Round((this.Valor / this.Parcelas), 2);

        public void MarcarComoProcessado()
        {
            this.Processado = true;
        }
    }
}
