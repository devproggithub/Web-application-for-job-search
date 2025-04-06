// RecruteurController.cs
using EmploiApp.Data;
using EmploiApp.Models;
using EmploiApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmploiApp.Controllers
{
    [Authorize(Roles = "Recruteur")]
    public class RecruteurController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOffreEmploiRepository _offreRepository;
        private readonly ApplicationDbContext _context;

        public RecruteurController(UserManager<ApplicationUser> userManager, IOffreEmploiRepository offreRepository, ApplicationDbContext context)
        {
            _userManager = userManager;
            _offreRepository = offreRepository;
            _context = context;
        }

        public IActionResult Accueil()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var recruteur = await _context.Recruteurs
                .Include(r => r.Offres)
                .ThenInclude(o => o.Secteur)
                .FirstOrDefaultAsync(r => r.UtilisateurId == userId);

            if (recruteur == null)
                return NotFound();

            return View(recruteur.Offres);
        }

        public async Task<IActionResult> Profil()
        {
            var userId = _userManager.GetUserId(User);
            var recruteur = await _context.Recruteurs.FirstOrDefaultAsync(r => r.UtilisateurId == userId);
            if (recruteur == null)
                return NotFound();

            return View(recruteur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profil(Recruteur model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = _userManager.GetUserId(User);
            var recruteur = await _context.Recruteurs.FirstOrDefaultAsync(r => r.UtilisateurId == userId);

            if (recruteur == null)
                return NotFound();

            recruteur.Entreprise = model.Entreprise;
            recruteur.Nom = model.Nom;
            recruteur.Prenom = model.Prenom;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Profil mis à jour avec succès.";
            return RedirectToAction(nameof(Profil));
        }

        public IActionResult Create()
        {
            ViewBag.Secteurs = new SelectList(_context.Secteurs.ToList(), "Id", "NomSecteur");
            return View(new OffreEmploi());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OffreEmploi offre)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Secteurs = new SelectList(_context.Secteurs.ToList(), "Id", "NomSecteur");
               // return View(offre);
            }

            var user = await _userManager.GetUserAsync(User);
            var recruteur = await _context.Recruteurs.FirstOrDefaultAsync(r => r.UtilisateurId == user.Id);

            if (recruteur == null)
                return NotFound();

            offre.RecruteurId = recruteur.Id_Recruteur;
            offre.DatePublication = DateTime.Now;

            await _offreRepository.AddAsync(offre);
            await _offreRepository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var offre = await _offreRepository.GetByIdAsync(id);
            if (offre == null)
                return NotFound();

            ViewBag.Secteurs = new SelectList(_context.Secteurs.ToList(), "Id", "NomSecteur", offre.SecteurId);
            return View(offre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OffreEmploi offre)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Secteurs = new SelectList(_context.Secteurs.ToList(), "Id", "NomSecteur", offre.SecteurId);
                return View(offre);
            }

            await _offreRepository.UpdateAsync(offre);
            await _offreRepository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var offre = await _offreRepository.GetByIdAsync(id);
            if (offre == null)
                return NotFound();

            return View(offre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offre = await _offreRepository.GetByIdAsync(id);
            if (offre != null)
            {
                await _offreRepository.DeleteAsync(offre);
                await _offreRepository.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}