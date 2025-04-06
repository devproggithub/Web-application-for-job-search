using EmploiApp.Models;

namespace EmploiApp.Repositories
{
    public interface ICVRepository:IRepository<CV>
    {
        /// <summary>
        /// Récupère tous les CVs d'un candidat spécifique
        /// </summary>
        /// <param name="idCandidat">Identifiant du candidat</param>
        /// <returns>Liste des CVs du candidat</returns>
        Task<IEnumerable<CV>> GetCVsByCandidatAsync(int? idCandidat);
        Task<CV> GetByIdAsync(int id);
    }
}
