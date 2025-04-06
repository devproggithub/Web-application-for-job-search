using EmploiApp.Data;
using EmploiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploiApp.Repositories
{
    public class SecteurRepository : Repository<Secteur>, ISecteurRepository
    {
        private readonly ApplicationDbContext _context;

        public SecteurRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Secteur>> GetAllSecteursAsync()
        {
            return await _context.Secteurs.ToListAsync();
        }

 
    }
}
