using System.ComponentModel.DataAnnotations;

namespace EmploiApp.Models
{
    public class Secteur
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomSecteur { get; set; }

        public string Description { get; set; }

        // Relation avec OffreEmploi (One-to-Many)
        public ICollection<OffreEmploi> Offres { get; set; } = new List<OffreEmploi>();
    }

}
