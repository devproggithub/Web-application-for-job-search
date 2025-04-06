using EmploiApp.Models;

namespace EmploiApp.Repositories
{
    public interface ISecteurRepository : IRepository<Secteur>
    {
        Task<IEnumerable<Secteur>> GetAllSecteursAsync();
    }
}
