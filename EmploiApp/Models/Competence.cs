using System.ComponentModel.DataAnnotations;

namespace EmploiApp.Models
{
    public class Competence
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomCompetence { get; set; }

        public string Description { get; set; }

        // Relation Many-to-Many avec CV via Competence_CV
        public ICollection<CompetenceCV> CompetenceCVs { get; set; }
    }

}
