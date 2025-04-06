using EmploiApp.Data;

namespace EmploiApp.Models.Factory
{
    public class RecruteurProfileHandler : IRoleProfileHandler
    {
        private readonly ApplicationDbContext _context;

        public RecruteurProfileHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task HandleProfileAsync(ApplicationUser user, string role, string returnUrl)
        {
            if (role == "Recruteur")
            {
                var recruteur = new Recruteur
                {
                    
                    UtilisateurId = user.Id
                    
                };
                _context.Recruteurs.Add(recruteur);
                await _context.SaveChangesAsync();
            }
            // Redirection vers la page de profil du recruteur
            // return Redirect logic handled externally
        }
    }
}
