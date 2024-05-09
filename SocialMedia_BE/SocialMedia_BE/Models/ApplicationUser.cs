using Microsoft.AspNetCore.Identity;

namespace SocialMedia_BE.Models
{
	public class ApplicationUser : IdentityUser
	{
        public int PostLimitNumber { get; set; }

		public virtual ICollection<IdentityRole> Roles { get; } = new List<IdentityRole>();

	}
}
