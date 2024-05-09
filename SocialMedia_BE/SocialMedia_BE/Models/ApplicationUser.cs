using Microsoft.AspNetCore.Identity;

namespace SocialMedia_BE.Models
{
	public class ApplicationUser : IdentityUser
	{
        public int PostLimitNumber { get; set; }
    }
}
