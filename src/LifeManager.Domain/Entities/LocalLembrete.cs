namespace LifeManager.Domain.Entities
{
    public class LocalLembrete
    {
        public int LocalId { get; set; }

        public Local Local { get; set; }

        public Lembrete Lembrete { get; set; }

        public int LembreteId { get; set; }
    }
}
