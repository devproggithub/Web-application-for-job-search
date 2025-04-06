using EmploiApp.Data;
using EmploiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploiApp.Repositories
{
    public class OffreEmploiRepository : Repository<OffreEmploi>, IOffreEmploiRepository
    {
        private readonly ApplicationDbContext _context;
        public OffreEmploiRepository(ApplicationDbContext context) : base(context) {

            _context = context;
        }

        public async Task<IEnumerable<OffreEmploi>> SearchAsync(string searchTerm, int? secteurId)
        {
            IQueryable<OffreEmploi> query = _context.OffresEmploi.Include(o => o.Secteur);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(o => o.TitreOffre.Contains(searchTerm) ||
                                         o.Description.Contains(searchTerm));
            }
            if (secteurId.HasValue)
            {
                query = query.Where(o => o.SecteurId == secteurId.Value);
            }
            return await query.ToListAsync();
        }
        public async Task<OffreEmploi> GetByIdAsync(int id)
        {
            return await _context.OffresEmploi
                .Include(o => o.Secteur)
                .Include(o => o.Recruteur)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<OffreEmploi>> GetOffresByCandidatIdAsync(int candidatId)
        {
            return await _context.Candidatures
                .Where(c => c.CV.CandidatId == candidatId) // Vérifier que le CV appartient au candidat
                .Select(c => c.OffreEmploi) // Sélectionner les offres liées aux candidatures
                .Distinct() // Éviter les doublons
                .ToListAsync();
        }

    }

}
