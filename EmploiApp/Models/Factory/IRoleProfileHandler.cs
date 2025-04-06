namespace EmploiApp.Models.Factory
{
    public interface IRoleProfileHandler
    {
        Task HandleProfileAsync(ApplicationUser user, string role, string returnUrl);
    }

}
