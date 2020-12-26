using LifeManager.API.DTO;
using System.Collections.Generic;

namespace LifeManager.API.DTO
{ 
    public class LembreteDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public decimal Qtd { get; set; }
        public bool Concluido { get; set; }
        public List<LocaisDTO> Locais { get; set; }
    }
}