using EmploiApp.Models;

namespace EmploiApp.Repositories
{
    public interface ICandidatureRepository:IRepository<Candidature>
    {
        Task<bool> ExisteCandidatureAsync(int idOffre, int idCV);
    }
}
