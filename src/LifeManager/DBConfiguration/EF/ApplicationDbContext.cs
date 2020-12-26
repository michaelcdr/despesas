using LifeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeManager.Infra.EF
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<DespesaMensal> DespesasMensais { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Lembrete> Lembretes { get; set; }
        public DbSet<LocalLembrete> LocaisLembretes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Despesa>().ToTable("Despesas")
                .HasMany(e => e.DespesasMensais)
                .WithOne(d => d.Despesa)
                .HasPrincipalKey(e =>e.Id)
                .HasForeignKey(e => e.DespesaId);

            modelBuilder.Entity<DespesaMensal>().ToTable("DespesasMensais")
                .HasOne(e => e.Despesa)
                .WithMany(e => e.DespesasMensais)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Lembrete>()
               .HasMany(e => e.Locais)
               .WithMany(e => e.Lembretes)
               .UsingEntity<LocalLembrete>(
               pt => pt
                   .HasOne(p => p.Local)
                   .WithMany()
                   .HasForeignKey("LocalId"),
               pt => pt
                   .HasOne(p => p.Lembrete)
                   .WithMany()
                   .HasForeignKey("LembreteId"))
               .ToTable("LocaisLembretes")
               .HasKey(pt => new { pt.LocalId, pt.LembreteId });
        }
    }
}
