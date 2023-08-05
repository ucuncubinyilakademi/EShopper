using Microsoft.AspNetCore.Identity;

namespace EShopper.WebApp.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
