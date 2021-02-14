using Dapper;
using LifeManager.Domain.Entities;
using LifeManager.Domain.Repositorios;
using LifeManager.Infra.DBConfiguration;
using LifeManager.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeManager.Infra.Repositorios.Dapper
{
    public class DespesasRepositorio : BaseRepository , IDespesaRepositorio
    {
        public DespesasRepositorio(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        public void Dispose()
        {
            Conexao.Close();
            Conexao.Dispose();
            GC.SuppressFinalize(Conexao);
        }

        public async Task<List<Despesa>> ObterTodasNaoPagas()
        {
            var despesas = (await Conexao.QueryAsync<Despesa>
                ("SELECT * FROM Despesas WHERE  Processado = 0 ORDER BY datacadastro desc ")).AsList();

            return despesas;
        }

        public async Task<int> Adicionar(Despesa despesa)
        {
            var result =  await Conexao.QueryAsync<int>(
                @"INSERT into Despesas(DataCadastro,DiaVencimento,Descritivo,Parcelas,Titulo,Valor,Processado,DataInicio,Recorrente) 
                values (GETDATE(),@DiaVencimento,@Descritivo,@Parcelas,@Titulo,@Valor,0,@DataInicio,@Recorrente); 
                SELECT CAST(SCOPE_IDENTITY() as int);", 
                new { despesa.DataCadastro, despesa.DiaVencimento, despesa.Descritivo, despesa.Parcelas, despesa.Titulo, despesa.Valor, despesa.DataInicio, despesa.Recorrente });
             
            return result.Single();
        }

        public async Task AdicionarDespesaMensal(DespesaMensal despesaMensal)
        {
            var parametros = new { despesaMensal.DespesaId, despesaMensal.Valor, despesaMensal.DataVencimento };
           await Conexao.QueryAsync<int>(
                "insert into DespesasMensais(despesaid, valor, pago, datavencimento) values(@DespesaId, @Valor, 0, @DataVencimento);",
                parametros
            );
        }

        public async Task Deletar(int id)
        {
            await Conexao.ExecuteAsync("delete FROM Despesas where id = @id", new { id });
        }

        public async Task<Despesa> ObterPorId(int id)
        {
            return await Conexao.QuerySingleOrDefaultAsync<Despesa>(@"SELECT * FROM Despesas WHERE id = @id", new { id= id });
        }

        public async Task<IEnumerable<Despesa>> ObterTodas()
        {
            var despesas = (await Conexao.QueryAsync<Despesa>
                (@"SELECT id, DiaVencimento, parcelas ,Descritivo, titulo, valor, Processado, DataCadastro 
                   FROM Despesas ORDER BY datacadastro desc ")).AsList();
            return despesas;
        }

        public async Task<List<Despesa>> ObterNaoProcessadasSemRecorrencia()
        {
            var despesas = (await Conexao.QueryAsync<Despesa>
               (@"SELECT id, DiaVencimento, parcelas ,Descritivo, titulo, valor, Processado, DataCadastro 
                  FROM Despesas where Processado = 0 and recorrente = 0 ORDER BY datacadastro desc ")).AsList();

            return despesas;
        }

        public async Task<List<Despesa>> ObterComRecorrencia()
        {
            var despesas = (await Conexao.QueryAsync<Despesa>
               (@"SELECT id, DiaVencimento, parcelas ,Descritivo, titulo, valor, Processado, DataCadastro 
                  FROM Despesas where recorrente = 1 ORDER BY datacadastro desc ")).AsList();

            return despesas;
        }

        public async Task MarcarComoProcessado(int id)
        {
            await Conexao.ExecuteAsync("update Despesas set processado = 1 where id = @id", new { id });
        }
    }
}
