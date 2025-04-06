using EmploiApp.Models;

namespace EmploiApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<OffreEmploi> Offres { get; set; }
        public IEnumerable<Secteur> Secteurs { get; set; }
        public string Keyword { get; set; }
        public int? SecteurId { get; set; }
    }
}
