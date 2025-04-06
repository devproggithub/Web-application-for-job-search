using EmploiApp.Data;

namespace EmploiApp.Models.Factory
{
    public class CandidatProfileHandler : IRoleProfileHandler
    {
        private readonly ApplicationDbContext _context;

        public CandidatProfileHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task HandleProfileAsync(ApplicationUser user, string role, string returnUrl)
        {
            if (role == "Candidat")
            {
                var candidate = new Candidat
                {
                    UtilisateurId = user.Id,
                    Telephone = string.Empty, // À compléter plus tard
                    Adresse = string.Empty,  // À compléter plus tard
                    Date_Naissance = DateTime.MinValue // Placeholder
                };
                _context.Candidats.Add(candidate);
                await _context.SaveChangesAsync();
            }
            // Redirection vers la page de profil du candidat
            // return Redirect logic handled externally
        }
    }

}
