namespace EmploiApp.Models
{
    public class CompetenceCV
    {
        // Clé composite composée de CompetenceId et CVId

        public int CompetenceId { get; set; }
        public Competence Competence { get; set; }

        public string Niveau { get; set; } // niveau de maitrise

        public int CVId { get; set; }
        public CV CV { get; set; }
    }
}
