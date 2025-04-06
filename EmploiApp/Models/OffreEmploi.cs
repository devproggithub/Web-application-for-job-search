using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploiApp.Models
{
    public class OffreEmploi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string TitreOffre { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Localisation { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeContrat { get; set; }

        public DateTime DatePublication { get; set; } = DateTime.Now;
        public DateTime? DateExpiration { get; set; }

        // Relation avec Secteur
        public int SecteurId { get; set; }
        public Secteur Secteur { get; set; }

        // Relation avec Recruteur
        public int RecruteurId { get; set; }
        public Recruteur Recruteur { get; set; }

        public ICollection<Candidature> Candidatures { get; set; }
    }

}
