using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploiApp.Models
{
    public class Recruteur
    {
        [Key]
        public int Id_Recruteur { get; set; }

        [MaxLength(100)]
        public string? Entreprise { get; set; }

        [MaxLength(50)]
        public string? Nom { get; set; }

        [MaxLength(50)]
        public string? Prenom { get; set; }

        // Clé étrangère vers ApplicationUser (Identity)
        [Required]
        public string UtilisateurId { get; set; }

        [ForeignKey("UtilisateurId")]
        public virtual ApplicationUser Utilisateur { get; set; }

        // Liste des offres publiées par ce recruteur
        public virtual ICollection<OffreEmploi> Offres { get; set; } = new List<OffreEmploi>();
    }
}
