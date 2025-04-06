using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmploiApp.Repositories;
using EmploiApp.Models;
using Microsoft.AspNetCore.Identity;
using EmploiApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmploiApp.Controllers
{
    public class OffreEmploiController : Controller
    {
        private readonly IOffreEmploiRepository _offreRepository;
        private readonly ICVRepository _cvRepository;
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public OffreEmploiController(IOffreEmploiRepository offreRepository, ICandidatureRepository candidatureRepository, ICVRepository cvRepository, UserManager<ApplicationUser> userManager)
        {
            _offreRepository = offreRepository;
            _candidatureRepository = candidatureRepository;
            _cvRepository = cvRepository;
            _userManager = userManager;
        }
        // Action Details pour afficher le détail d'une offre
        public async Task<IActionResult> Details(int id)
        {
            var offre = await _offreRepository.GetByIdAsync(id);
            if (offre == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users
                            .Include(u => u.Candidat)
                            .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null || user.Candidat == null)
            {
                return Redirect("/Identity/Account/Login"); // ou RedirectToAction("Login", "Account") si tu veux
            }

            var cvs = await _cvRepository.GetCVsByCandidatAsync(user.Candidat.Id_Candidat);

            var viewModel = new OffreDetailsViwModel
            {
                Offre = offre,
                Cvs = cvs
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Postuler(int idOffre, int cvId)
        {
            // Récupérer l'utilisateur connecté avec sa relation Candidat chargée
            var user = await _userManager.Users
                           .Include(u => u.Candidat)
                           .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));


            if (user == null)
            {
                return Unauthorized();
                // return RedirectToAction("Login", "Account");
            }
            // Vérifier que l'utilisateur possède un profil candidat
            if (user.Candidat == null)
            {
                TempData["ErrorMessage"] = "Votre profil candidat n'est pas complété.";
                return RedirectToAction("Details", new { id = idOffre });
            }

            // Vérifier que le même CV n'a pas déjà postulé à cette offre
            if (await _candidatureRepository.ExisteCandidatureAsync(idOffre, cvId))
            {
                TempData["ErrorMessage"] = "Vous avez déjà postulé à cette offre avec ce CV.";
                return RedirectToAction("Details", new { id = idOffre });
            }


            var candidature = new Candidature
            {
                
                IdOffre = idOffre,
                IdCV = cvId,
                DatePostulation = DateTime.Now,
                Status = "En cours"
            };

            await _candidatureRepository.AddAsync(candidature);
            await _candidatureRepository.SaveAsync();

            TempData["SuccessMessage"] = "Votre candidature a été envoyée avec succès !";
            return RedirectToAction("Details", new { id = idOffre });
        }
        public async Task<IActionResult> OffresPostulees()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var offres = await _offreRepository.GetOffresByCandidatIdAsync(int.Parse(userId));
            return View(offres);
        }



        // GET: OffreController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OffreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OffreController/Create
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

        // GET: OffreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OffreController/Edit/5
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

        // GET: OffreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OffreController/Delete/5
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
