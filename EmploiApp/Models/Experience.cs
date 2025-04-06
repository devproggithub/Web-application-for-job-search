using System.ComponentModel.DataAnnotations;

namespace EmploiApp.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Poste { get; set; }

        [Required]
        [MaxLength(100)]
        public string Entreprise { get; set; }

        public DateTime DateDebut { get; set; }
        
        public DateTime DateFin { get; set; }

        public string Description { get; set; }

        // Relation avec CV (One-to-Many)
        public int CVId { get; set; }
        public CV CV { get; set; }
    }

}
