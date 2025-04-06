namespace EmploiApp.Models.Factory
{
    public class RoleProfileHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RoleProfileHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRoleProfileHandler GetHandler(string role)
        {
            if (role == "Candidat")
            {
                return _serviceProvider.GetService<CandidatProfileHandler>();
            }
            else if (role == "Recruteur")
            {
                return _serviceProvider.GetService<RecruteurProfileHandler>();
            }
            else
            {
                throw new ArgumentException($"Rôle non pris en charge : {role}");
            }
        }
    }

}
