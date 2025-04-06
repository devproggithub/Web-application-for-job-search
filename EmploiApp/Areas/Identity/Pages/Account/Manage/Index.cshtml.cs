// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EmploiApp.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmploiApp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Téléphone")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Nom")]
            public string? Nom { get; set; }

            [Display(Name = "Prénom")]
            public string? Prenom { get; set; }

            [Display(Name = "Adresse")]
            public string? Adresse { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "Date de naissance")]
            public DateTime? DateNaissance { get; set; }

            [Display(Name = "Photo de profil")]
            public IFormFile? PhotoFile { get; set; } // Pour l'upload

            public byte[]? Photo { get; set; } // Pour afficher l'image stockée

            public string? Email { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            //var userName = await _userManager.GetUserNameAsync(user);
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //Username = userName;

            Input = new InputModel
            {
                Nom = user.Candidat.Nom,
                Prenom = user.Candidat.Prenom,
                Adresse = user.Candidat.Adresse,
                DateNaissance = user.Candidat.Date_Naissance,
                Photo = user.Candidat.Photo,
                Email = user.Email,
                PhoneNumber = user.Candidat.Telephone
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.Users
                   .Include(u => u.Candidat)
                   .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.Users
                   .Include(u => u.Candidat)
                   .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            // Mettre à jour les champs
            user.Candidat.Nom = Input.Nom;
            user.Candidat.Prenom = Input.Prenom;
            user.Candidat.Adresse = Input.Adresse;
            user.Candidat.Date_Naissance = Input.DateNaissance;
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            user.Candidat.Telephone = Input.PhoneNumber;

            // Gérer l'upload du fichier et le stocker en binaire
            if (Input.PhotoFile != null && Input.PhotoFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await Input.PhotoFile.CopyToAsync(ms);
                user.Candidat.Photo = ms.ToArray(); // Convertir en binaire et stocker
            }
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                // Recharger la page
                await OnGetAsync();
                return Page();
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Votre profil est à jour";
            return RedirectToPage();
        }
    }
}
