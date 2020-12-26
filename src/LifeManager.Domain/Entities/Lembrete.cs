using System.Collections.Generic;

namespace LifeManager.Domain.Entities
{
    public class Lembrete
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public decimal Qtd { get; set; }
        public bool Concluido { get; set; }
        public List<Local> Locais { get; set; }
    }
}
