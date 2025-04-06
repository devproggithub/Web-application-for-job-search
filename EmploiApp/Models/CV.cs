using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmploiApp.Models
{
    public class CV
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Titre { get; set; }

        public string Description { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        // Relation avec Candidat (Utilisateur)
        [Required]
        public int CandidatId { get; set; }

        [ForeignKey("CandidatId")]
        public Candidat Candidat { get; set; }

        // Collection d'expériences
        public ICollection<Experience> Experiences { get; set; }

        // Collection de diplômes liés via DiplomeCV
        public ICollection<DiplomeCV> DiplomeCVs { get; set; }
        // Collection dde copétences liés via CompétencesCVs
        public ICollection<CompetenceCV> CompetenceCVs { get; set; }

        // Collection de langues liés via langueCV
        public ICollection<LangueCV> LangueCVs { get; set; }

        public ICollection<Candidature> Candidatures { get; set; }

        public string FichierPath { get; set; }

    }

}
