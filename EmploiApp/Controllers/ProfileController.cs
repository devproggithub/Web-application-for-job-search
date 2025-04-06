using EmploiApp.Data;
using EmploiApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EmploiApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: /Profile/CompleteCandidateProfile
        public async Task<IActionResult> CompleteCandidateProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var candidate = await _context.Candidats.FirstOrDefaultAsync(c => c.UtilisateurId == user.Id);
            if (candidate == null)
            {
                candidate = new Candidat { UtilisateurId = user.Id }; // Créer un objet par défaut
                _context.Candidats.Add(candidate);
                await _context.SaveChangesAsync();
            }

            var model = new Candidat
            {
                Telephone = candidate.Telephone,
                Adresse = candidate.Adresse,
                Date_Naissance = candidate.Date_Naissance
            };

            return View(model);
        }

        // POST: /Profile/CompleteCandidateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteCandidateProfile(Candidat model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var candidate = await _context.Candidats.FirstOrDefaultAsync(c => c.UtilisateurId == user.Id);
            if (candidate != null)
            {
                candidate.Telephone = model.Telephone;
                candidate.Adresse = model.Adresse;
                candidate.Date_Naissance = model.Date_Naissance;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // Après la complétion, redirigez vers l'accueil
            }

            return View(model); // Réaffiche la vue en cas d'erreur
        }

        // GET: /Profile/CompleteRecruiterProfile
        public async Task<IActionResult> CompleteRecruiterProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var recruiter = await _context.Recruteurs.FirstOrDefaultAsync(r => r.UtilisateurId == user.Id);
            if (recruiter == null)
            {
                recruiter = new Recruteur { UtilisateurId = user.Id }; // Créer un objet par défaut
                _context.Recruteurs.Add(recruiter);
                await _context.SaveChangesAsync();
            }

            var model = new Recruteur
            {
                Entreprise = recruiter.Entreprise,
            };

            return View(model);
        }

        // POST: /Profile/CompleteRecruiterProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteRecruiterProfile(Recruteur model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var recruiter = await _context.Recruteurs.FirstOrDefaultAsync(r => r.UtilisateurId == user.Id);
            if (recruiter != null)
            {
                recruiter.Entreprise = model.Entreprise;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // Après la complétion, redirigez vers l'accueil
            }

            return View(model); // Réaffiche la vue en cas d'erreur
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
