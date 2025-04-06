using EmploiApp.Models;
using EmploiApp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmploiApp.Areas.Identity.Pages.Account.Manage
{
    public class ManageCVModel : PageModel
    {
        private readonly ICVRepository _cvRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageCVModel(ICVRepository cvRepository, UserManager<ApplicationUser> userManager)
        {
            _cvRepository = cvRepository;
            _userManager = userManager;
        }

        public IEnumerable<CV> CVs { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.Users
               .Include(u => u.Candidat)
               .Include(u => u.Candidat.CVs)
               .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user != null && user.Candidat != null)
            {
                CVs = await _cvRepository.GetCVsByCandidatAsync(user.Candidat.Id_Candidat);
            }
            else
            {
                CVs = new List<CV>();
            }
        }

        // Handler pour ajouter un CV avec fichier
        public async Task<IActionResult> OnPostAddCVAsync(IFormFile FichierCV, string Titre, string Description)
        {
            var user = await _userManager.Users
                .Include(u => u.Candidat)
                .Include(u => u.Candidat.CVs)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null || user.Candidat == null)
                return Unauthorized();

            if (FichierCV == null || FichierCV.Length == 0)
                return BadRequest("Aucun fichier n'a été sélectionné.");

            // Générer un nom de fichier unique
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(FichierCV.FileName)}";
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await FichierCV.CopyToAsync(stream);
            }

            var cv = new CV
            {
                Titre = Titre,
                Description = Description,
                FichierPath = $"/uploads/{fileName}",
                DateCreation = DateTime.Now,
                CandidatId = user.Candidat.Id_Candidat
            };

            await _cvRepository.AddAsync(cv);
            await _cvRepository.SaveAsync();

            return new JsonResult(new { success = true });
        }
    }
}
