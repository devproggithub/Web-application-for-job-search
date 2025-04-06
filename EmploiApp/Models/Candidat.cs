using EmploiApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Candidat
{
    [Key]
    public int Id_Candidat { get; set; }

    [MaxLength(20)]
    public string? Nom { get; set; }

    [MaxLength(20)]
    public string? Prenom { get; set; }

    [MaxLength(20)]
    public string? Telephone { get; set; }


    [MaxLength(255)]
    public string? Adresse { get; set; }

    public byte[]? Photo { get; set; }  // Stockage de l'image


    [DataType(DataType.Date)]
    public DateTime? Date_Naissance { get; set; }

    // Clé étrangère vers ApplicationUser
    public string UtilisateurId { get; set; }


    // Un candidat peut avoir plusieurs CVs
    public ICollection<CV> CVs { get; set; } = new List<CV>();

    [ForeignKey("UtilisateurId")]
    public virtual ApplicationUser Utilisateur { get; set; }
}
