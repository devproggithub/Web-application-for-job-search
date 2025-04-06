namespace EmploiApp.Models
{
    public class LangueCV
    {
        // Clé composite composée de LangueId et CVId

        public int LangueId { get; set; }
        public Langue Langue { get; set; }

        public string Niveau { get; set; }

        public int CVId { get; set; }
        public CV CV { get; set; }
    }
}
