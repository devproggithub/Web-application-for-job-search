using EmploiApp.Data;
using EmploiApp.Models;
using EmploiApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmploiApp.Areas.Identity.Pages.Account.Manage
{
    public class EditCVModel : PageModel
    {
        private readonly ICVRepository _cvRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public EditCVModel(ICVRepository cvRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _cvRepository = cvRepository;
            _userManager = userManager;
            _context = context;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public CV CV { get; set; }

        // Listes disponibles pour les dropdowns
        public IEnumerable<Diplome> AvailableDiplomes { get; set; }
        public IEnumerable<Competence> AvailableCompetences { get; set; }
        public IEnumerable<Langue> AvailableLangues { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(100)]
            public string Titre { get; set; }
            public string Description { get; set; }

            // Ces champs recevront des données JSON pour les associations
            public string DiplomesJson { get; set; }
            public string CompetencesJson { get; set; }
            public string LanguesJson { get; set; }
            public string ExperiencesJson { get; set; }
        }
        [Authorize(Roles = "Candidat")]

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userManager.Users
               .Include(u => u.Candidat)
               .Include(u => u.Candidat.CVs)
               .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
                return Unauthorized();

            CV = await _context.CVs
        .Include(c => c.DiplomeCVs)
        .Include(c => c.CompetenceCVs)
        .Include(c => c.LangueCVs)
        .Include(c=>c.Experiences)
        .FirstOrDefaultAsync(c => c.Id == id && c.CandidatId == user.Candidat.Id_Candidat);
            if (CV == null || CV.CandidatId != user.Candidat.Id_Candidat)
                return NotFound();

            // Charger les listes disponibles
            AvailableDiplomes = await _context.Diplomes.ToListAsync();
            AvailableCompetences = await _context.Competences.ToListAsync();
            AvailableLangues = await _context.Langues.ToListAsync();

            Input = new InputModel
            {
                Titre = CV.Titre,
                Description = CV.Description,
                DiplomesJson = JsonConvert.SerializeObject(CV.DiplomeCVs, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DateFormatString = "dd/MM/yyyy"
                }),
                CompetencesJson = JsonConvert.SerializeObject(CV.CompetenceCVs, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DateFormatString = "dd/MM/yyyy"
                    
                }),
                LanguesJson = JsonConvert.SerializeObject(CV.LangueCVs, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateFormatString = "dd/MM/yyyy" }),
                ExperiencesJson = JsonConvert.SerializeObject(CV.Experiences, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateFormatString = "dd/MM/yyyy" })
            };

            return Page();
        }
        [Authorize(Roles = "Candidat")]

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.Users
               .Include(u => u.Candidat)
               .Include(u => u.Candidat.CVs)
               .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
                return Unauthorized();

            CV = await _context.CVs
        .Include(c => c.DiplomeCVs)
        .Include(c => c.CompetenceCVs)
        .Include(c => c.LangueCVs)
        .Include(c => c.Experiences)
        .FirstOrDefaultAsync(c => c.Id == id && c.CandidatId == user.Candidat.Id_Candidat);
            if (CV == null || CV.CandidatId != user.Candidat.Id_Candidat)
                return NotFound();

            if (!ModelState.IsValid)
            {
                // Rechargez les listes disponibles
                AvailableDiplomes = await _context.Diplomes.ToListAsync();
                AvailableCompetences = await _context.Competences.ToListAsync();
                AvailableLangues = await _context.Langues.ToListAsync();
                return Page();
            }

            // Mettre à jour les informations de base du CV
            CV.Titre = Input.Titre;
            CV.Description = Input.Description;

            // Mettre à jour les associations
            // Vous pouvez personnaliser la désérialisation selon la structure de vos classes d'association
            CV.DiplomeCVs = JsonConvert.DeserializeObject<List<DiplomeCV>>(Input.DiplomesJson);
            CV.CompetenceCVs = JsonConvert.DeserializeObject<List<CompetenceCV>>(Input.CompetencesJson);
            CV.LangueCVs = JsonConvert.DeserializeObject<List<LangueCV>>(Input.LanguesJson);
            CV.Experiences = JsonConvert.DeserializeObject<List<Experience>>(Input.ExperiencesJson);

            await _cvRepository.UpdateAsync(CV);
            await _cvRepository.SaveAsync();

            TempData["StatusMessage"] = "Votre CV a été mis à jour avec succès.";
            return RedirectToPage("./ManageCV");
        }
    }
}
