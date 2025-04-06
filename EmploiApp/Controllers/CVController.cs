using EmploiApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmploiApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EmploiApp.Controllers
{
    public class CVController : Controller
    {
        private readonly ICVRepository _cvRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CVController(ICVRepository cvRepository, UserManager<ApplicationUser> userManager)
        {
            _cvRepository = cvRepository;
            _userManager = userManager;
        }
        [Authorize(Roles = "Candidat")]

        public async Task<IActionResult> MesCVs()
        {
            var userId = _userManager.GetUserId(User); // Obtient l'ID de l'utilisateur connecté
            if (userId == null) return Unauthorized();

            var cvs = await _cvRepository.GetCVsByCandidatAsync(int.Parse(userId)); // Convertir en int si nécessaire
            return View(cvs);
        }
        // GET: CVController
        [Authorize(Roles = "Candidat")]

        public async Task<IActionResult> Index()
        {
            // Charger l'utilisateur avec sa relation candidat
            var user = await _userManager.Users
                .Include(u => u.Candidat)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user?.Candidat == null)
            {
                // L'utilisateur n'a pas encore de profil candidat complété
                return RedirectToAction("CompleteCandidateProfile", "Account");
            }

            // Utiliser l'ID du candidat (Candidat.Id_Candidat)
            var cvs = await _cvRepository.GetCVsByCandidatAsync(user.Candidat.Id_Candidat);
            return View(cvs);
        }

        // GET: CVController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CVController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CVController/Create
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

        // GET: CVController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        [Authorize(Roles = "Candidat")]

        // POST: CVController/Edit/5
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

        // GET: CVController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CVController/Delete/5
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
