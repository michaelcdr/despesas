using LifeManager.Domain.Entities;
using LifeManager.Domain.Repositorios;
using LifeManager.Infra.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeManager.Infra.Repositorios.EF
{
    public class DespesasRepositorio : IDespesasRepositorio
    {
        private readonly ApplicationDbContext _context;

        public DespesasRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DespesaMensal>> ObterTodasNaoPagas()
        {
            return await _context.DespesasMensais.Where(e => !e.Pago).OrderBy(e => e.DataVencimento).ToListAsync();
        }

        public async Task<int> Adicionar(Despesa despesa)
        {
            _context.Despesas.Add(despesa);
            await _context.SaveChangesAsync();
            return despesa.Id;
        }

        public async Task AdicionarDespesaMensal(DespesaMensal despesaMensal)
        {
            _context.DespesasMensais.Add(despesaMensal);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            _context.DespesasMensais.RemoveRange(_context.DespesasMensais.Where(e => e.DespesaId == id));
            _context.Despesas.Remove(_context.Despesas.Find(id));
            await _context.SaveChangesAsync();
        }

        public async Task<Despesa> ObterPorId(int id)
            => await _context.Despesas.Include(e=>e.DespesasMensais).SingleAsync(e => e.Id == id);
        

        public async Task<IEnumerable<Despesa>> ObterTodas()
            => await _context.Despesas.OrderByDescending(e => e.DataCadastro).ToListAsync();

        public async Task<List<Despesa>> ObterNaoProcessadasSemRecorrencia()
            => await _context.Despesas.Where(e => !e.Processado && !e.Recorrente).OrderByDescending(e => e.DataCadastro).ToListAsync();

        public async Task<List<Despesa>> ObterComRecorrencia()
            => await _context.Despesas.Where(e => !e.Processado &&  e.Recorrente).OrderByDescending(e => e.DataCadastro).ToListAsync();

        public async Task MarcarComoProcessado(int id)
        {
            Despesa despesa = await ObterPorId(id);
            despesa.MarcarComoProcessado();
            _context.Entry(despesa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteDespesaMensalAberta(DespesaMensal despesaMensal)
        {
            return await _context.DespesasMensais.AnyAsync(e => !e.Pago && despesaMensal.DataVencimento.Month == e.DataVencimento.Month && e.DespesaId == despesaMensal.DespesaId);
        }

        public async Task<decimal> ObterGastosMensal()
        {
            return await _context.DespesasMensais.Where(e => !e.Pago && e.DataVencimento.Month == DateTime.Now.Month).SumAsync(e=>e.Valor);
        }

        public async Task<bool> Existe(int id)
        {
            return await _context.Despesas.AnyAsync(despesa => despesa.Id == id);
        }

        public async Task Atualizar(Despesa despesa)
        {
            _context.Entry(despesa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}