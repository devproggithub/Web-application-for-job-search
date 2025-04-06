using EmploiApp.Models;
using EmploiApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploiApp.Controllers
{
    public class CandidatController : Controller
    {
        private readonly IOffreEmploiRepository _offreRepository;
        private readonly ISecteurRepository _secteurRepository;

        public CandidatController(IOffreEmploiRepository offreRepository, ISecteurRepository secteurRepository)
        {
            _offreRepository = offreRepository;
            _secteurRepository = secteurRepository;
        }

        // Action qui affiche la page d'accueil du candidat 
        public async Task<IActionResult> Accueil(string search, int? secteurId)
        {
            // Récupérer la liste des secteurs depuis la base de données
            var secteurs = await _secteurRepository.GetAllSecteursAsync();
            ViewBag.Secteurs = secteurs;

            //IEnumerable<OffreEmploi> offres;
            //if (!string.IsNullOrEmpty(search) || secteurId.HasValue)
            //{
            //    offres = await _offreRepository.SearchAsync(search, secteurId);
            //}
            //else
            //{
            //    offres = await _offreRepository.GetAllAsync();
            //}

            // Récupérer les offres d'emploi avec filtrage éventuel
            var offres = await _offreRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                offres = offres.Where(o => o.TitreOffre.Contains(search) || o.Description.Contains(search)).ToList();
            }

            if (secteurId.HasValue && secteurId.Value > 0)
            {
                offres = offres.Where(o => o.SecteurId == secteurId.Value).ToList();
            }


            // On passe éventuellement les valeurs de recherche à la vue
            ViewBag.SearchTerm = search;
            ViewBag.SecteurId = secteurId;

            return View(offres);
        }

        // GET: CandidatController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CandidatController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CandidatController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CandidatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CandidatController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CandidatController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CandidatController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CandidatController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
