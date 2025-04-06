using System.ComponentModel.DataAnnotations;

namespace EmploiApp.Models
{
    public class Langue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NomLangue { get; set; }

        // Relation Many-to-Many avec CV
        public ICollection<LangueCV> LangueCVs { get; set; }
    }

}
