namespace EmploiApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Candidature
    {
       // [Key, Column(Order = 0)]
        public int IdOffre { get; set; }

        //[ForeignKey("IdOffre")]
        public OffreEmploi OffreEmploi { get; set; }

        //[Key, Column(Order = 1)]
        public int IdCV { get; set; }

        //[ForeignKey("IdCV")]
        public CV CV { get; set; }

        public DateTime DatePostulation { get; set; } = DateTime.Now;

        public string Status { get; set; } = "En cours"; // Valeur par défaut
    }


}
