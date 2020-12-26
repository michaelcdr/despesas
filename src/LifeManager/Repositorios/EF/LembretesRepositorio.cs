using LifeManager.Domain.Entities;
using LifeManager.Domain.Repositorios;
using LifeManager.Infra.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeManager.Infra.Repositorios.EF
{
    public class LembretesRepositorio : ILembretesRepositorio
    {
        private readonly ApplicationDbContext _context;

        public LembretesRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Lembrete>> ObterTodos()
        {
            return await _context.Lembretes.Include(a=>a.Locais).ToListAsync();
        }
    }
}
