using Microsoft.AspNetCore.Identity;

namespace SocialMedia_BE.Models
{
	public class DataInitializer
	{
		public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
			if (!adminRoleExists)
			{
				await roleManager.CreateAsync(new IdentityRole("Admin"));
				await roleManager.CreateAsync(new IdentityRole("User"));
			}

			var defaultUser = await userManager.FindByEmailAsync("admin@example.com");
			if (defaultUser == null)
			{
				var newUser = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com", PostLimitNumber=10 };
				await userManager.CreateAsync(newUser, "P@ssw0rd");

				await userManager.AddToRoleAsync(newUser, "Admin");
			}
		}
	}
}
