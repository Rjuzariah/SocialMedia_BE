using Microsoft.AspNetCore.Identity;

namespace SocialMedia_BE.Models
{
	public class ApplicationRole : IdentityRole
	{
		public ApplicationRole(string roleName) : base(roleName)
		{
		}

		// Navigation property for users associated with the role
		public virtual ICollection<ApplicationUser> Users { get; } = new List<ApplicationUser>();
	}
}
