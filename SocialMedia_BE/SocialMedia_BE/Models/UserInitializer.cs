using Microsoft.AspNetCore.Identity;

namespace SocialMedia_BE.Models
{
	public class UserInitializer
	{
		public static async Task InitializeAsync(UserManager<ApplicationUser> userManager)
		{
			var defaultUser = new ApplicationUser { UserName = "admin@mail.com", Email = "admin@mail.com" };

			// Create the user with a default password
			var result = await userManager.CreateAsync(defaultUser, "P@ssw0rd1");

			if (result.Succeeded)
			{
				// Assign "Admin" role to the default user
				var userrole = await userManager.AddToRoleAsync(defaultUser, "Admin");
			}
		}
	}
}
