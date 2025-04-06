using EmploiApp.Models;
using EmploiApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EmploiApp.Areas.Identity.Pages.Account.Manage
{
    public class DeleteCVModel : PageModel
    {
        private readonly ICVRepository _cvRepository;

        public DeleteCVModel(ICVRepository cvRepository)
        {
            _cvRepository = cvRepository;
        }

        [BindProperty]
        public CV CV { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CV = await _cvRepository.GetByIdAsync(id);
            if (CV == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CV == null)
                return NotFound();

            await _cvRepository.DeleteAsync(CV);
            await _cvRepository.SaveAsync();

            return RedirectToPage("ManageCV");
        }
    }
}
