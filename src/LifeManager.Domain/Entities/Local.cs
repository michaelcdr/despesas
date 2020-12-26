using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LifeManager.Domain.Entities
{
    public class Local
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Descritivo { get; set; }
        public List<Lembrete> Lembretes { get; set; }
    }
}
