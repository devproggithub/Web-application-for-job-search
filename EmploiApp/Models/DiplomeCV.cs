using System.ComponentModel.DataAnnotations;

namespace EmploiApp.Models
{
    public class DiplomeCV
    {
        public int DiplomeId { get; set; } // id_diplome
        public Diplome Diplome { get; set; }

        public string Niveau { get; set; } // Bac, DUT, Licence ...

        public DateTime DateObtentation { get; set; }

        public int CVId { get; set; } // id_cv
        public CV CV { get; set; }
    }
}
