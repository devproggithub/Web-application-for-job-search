using EmploiApp.Models;
using EmploiApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmploiApp.Controllers
{
    public class PublicOffreEmploiController : Controller
    {
        private readonly IOffreEmploiRepository _offreRepository;
        private readonly IRepository<Secteur> _secteurRepository;

        public PublicOffreEmploiController(IOffreEmploiRepository offreRepository, IRepository<Secteur> secteurRepository)
        {
            _offreRepository = offreRepository;
            _secteurRepository = secteurRepository;
        }

        // Action pour afficher la liste des offres avec filtres
        public async Task<IActionResult> Index(string keyword, int? secteurId)
        {
            var offres = await _offreRepository.SearchAsync(keyword, secteurId);
            var secteurs = await _secteurRepository.GetAllAsync();

            var viewModel = new HomeViewModel
            {
                Offres = offres,
                Secteurs = secteurs,
                Keyword = keyword,
                SecteurId = secteurId
            };

            return View(viewModel);
        }

        // Action pour afficher les détails d'une offre sans fonctionnalités de candidature
        public async Task<IActionResult> Details(int id)
        {
            var offre = await _offreRepository.GetByIdAsync(id);

            if (offre == null)
            {
                return NotFound();
            }

            // Utilisez directement le modèle OffreEmploi au lieu d'un ViewModel
            return View(offre);
        }

        // Action pour afficher les offres par secteur
        public async Task<IActionResult> BySecteur(int secteurId)
        {
            var offres = await _offreRepository.SearchAsync(null, secteurId);
            var secteur = await _secteurRepository.GetByIdAsync(secteurId);

            if (secteur == null)
            {
                return NotFound();
            }

            ViewBag.SecteurName = secteur.NomSecteur;

            return View(offres);
        }
    }
}
