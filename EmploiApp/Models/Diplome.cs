using System.ComponentModel.DataAnnotations;

namespace EmploiApp.Models
{
    public class Diplome
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string NomDiplome { get; set; }

        // Diplôme peut être lié à plusieurs CVs via Diplome_CV
        public ICollection<DiplomeCV> DiplomeCVs { get; set; }
    }

}
