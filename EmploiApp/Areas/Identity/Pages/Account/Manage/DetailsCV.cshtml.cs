using EmploiApp.Models;
using EmploiApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EmploiApp.Areas.Identity.Pages.Account.Manage
{
    public class DetailsCVModel : PageModel
    {
        private readonly ICVRepository _cvRepository;

        public DetailsCVModel(ICVRepository cvRepository)
        {
            _cvRepository = cvRepository;
        }

        public CV CV { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CV = await _cvRepository.GetByIdAsync(id);
            if (CV == null)
                return NotFound();

            return Page();
        }
    }
}
