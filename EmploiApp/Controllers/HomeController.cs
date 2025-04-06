using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmploiApp.Models;
using EmploiApp.Data;
using Microsoft.EntityFrameworkCore;


namespace EmploiApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context; // Injection du contexte de base de données
    }
    //hadi modifier
    public IActionResult Index(string keyword, int? secteurId)
    {
        // Récupérer les offres filtrées avec inclusion du Recruteur et Secteur
        var offres = _context.OffresEmploi
            .Include(o => o.Recruteur) // Include the Recruteur navigation property
            .Include(o => o.Secteur)  // Optional: Include the Secteur navigation property
            .Where(o => string.IsNullOrEmpty(keyword) || o.TitreOffre.Contains(keyword))
            .Where(o => !secteurId.HasValue || o.SecteurId == secteurId)
            .ToList();

        // Récupérer tous les secteurs
        var secteurs = _context.Secteurs.ToList();

        // Créer le ViewModel
        var viewModel = new HomeViewModel
        {
            Offres = offres,
            Secteurs = secteurs,
            Keyword = keyword,
            SecteurId = secteurId
        };

        // Assurez-vous que les listes sont initialisées pour éviter des erreurs
        if (viewModel.Offres == null)
            viewModel.Offres = new List<OffreEmploi>();
        if (viewModel.Secteurs == null)
            viewModel.Secteurs = new List<Secteur>();

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
