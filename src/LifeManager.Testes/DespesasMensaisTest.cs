using LifeManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LifeManager.Testes
{
    public class DespesasMensaisTest
    {
        [Fact]
        public void TotalDeDespesasDeveSerPositivo()
        {
            var dataInicioDespesa = new DateTime(2020, 12, DateTime.Now.Day);
            const int DiaVencimento = 15;
            const int Parcelas = 15;
            var despesa = new Despesa(0, DiaVencimento, Parcelas, "Vivo", "Conta da vivo", 74.94m, false, dataInicioDespesa);

            List<DespesaMensal> desepesasGeradas = despesa.GerarDespesas();

            bool ehPositivo = desepesasGeradas.Select(e => e.Valor).Sum() >= 0;

            Assert.True(ehPositivo);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        public void DeveGerarUmNumeroIgualAoNumeroEsperado(int dia, int parcelas, int numeroEsperadoDeDespesas)
        {
            var dataInicioDespesa = new DateTime(2020, 12, dia);

            var despesa = new Despesa(0, 15,parcelas, "Vivo", "Conta da vivo", 74.94m, false, dataInicioDespesa);

            List<DespesaMensal> desepesasGeradas = despesa.GerarDespesas();

            Assert.Equal(numeroEsperadoDeDespesas, desepesasGeradas.Count);
        }
    }
}
