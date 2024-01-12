using Microsoft.AspNetCore.Identity;

namespace my_bookss.Data.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Custom { get; set; }

    }
}
