using EmploiApp.Models;

namespace EmploiApp.Repositories
{
    public interface IOffreEmploiRepository : IRepository<OffreEmploi>
    {
        Task<IEnumerable<OffreEmploi>> SearchAsync(string searchTerm, int? secteurId);
        Task<OffreEmploi> GetByIdAsync(int id);
        Task<IEnumerable<OffreEmploi>> GetOffresByCandidatIdAsync(int candidatId);

    }
}
