using Microsoft.AspNetCore.Identity;

namespace EmploiApp.Models
{
    public class ApplicationUser:IdentityUser
    {

        // Relations one-to-one
        public virtual Candidat? Candidat { get; set; }
        public virtual Recruteur? Recruteur { get; set; }
    }
}
