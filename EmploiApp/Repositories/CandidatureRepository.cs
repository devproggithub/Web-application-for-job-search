using EmploiApp.Data;
using EmploiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploiApp.Repositories
{
    public class CandidatureRepository : Repository<Candidature>, ICandidatureRepository
    {
        private readonly ApplicationDbContext _context;
        public CandidatureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExisteCandidatureAsync(int idOffre, int idCV)
        {
            return await _context.Candidatures.AnyAsync(c => c.IdOffre == idOffre && c.IdCV == idCV);
        }
    }
}
