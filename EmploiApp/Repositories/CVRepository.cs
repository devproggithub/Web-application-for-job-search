using EmploiApp.Data;
using EmploiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploiApp.Repositories
{
    public class CVRepository : Repository<CV>, ICVRepository
    {
        private readonly ApplicationDbContext _context;

        public CVRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CV>> GetCVsByCandidatAsync(int? idCandidat)
        {
            return await _context.CVs
                .Where(cv => cv.CandidatId == idCandidat)
                .ToListAsync();
        }
        public async Task<CV> GetByIdAsync(int id)
        {
            return await _context.CVs
                .Include(cv => cv.DiplomeCVs)
                    .ThenInclude(dc => dc.Diplome)
                .Include(cv => cv.CompetenceCVs)
                    .ThenInclude(cc => cc.Competence)
                .Include(cv => cv.LangueCVs)
                    .ThenInclude(lc => lc.Langue)
                .Include(cv => cv.Experiences)
                .FirstOrDefaultAsync(cv => cv.Id == id);
        }
    }
}
