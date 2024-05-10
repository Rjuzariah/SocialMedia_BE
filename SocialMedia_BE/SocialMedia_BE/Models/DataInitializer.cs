using Microsoft.AspNetCore.Identity;

namespace SocialMedia_BE.Models
{
	public class DataInitializer
	{
		public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
			var userRoleExists = await roleManager.RoleExistsAsync("User");
			if (!adminRoleExists && !userRoleExists)
			{
				await roleManager.CreateAsync(new IdentityRole("Admin"));
				await roleManager.CreateAsync(new IdentityRole("User"));
			}

			var defaultAdmin = await userManager.FindByEmailAsync("admin@mail.com");
			if (defaultAdmin == null)
			{
				var newAdmin = new ApplicationUser { UserName = "admin", Email = "admin@mail.com", PostLimitNumber=10 };
				await userManager.CreateAsync(newAdmin, "P@ssw0rd");

				await userManager.AddToRoleAsync(newAdmin, "Admin");
			}
			var defaultUser = await userManager.FindByEmailAsync("user@mail.com");
			if (defaultUser == null)
			{
				var newAdmin = new ApplicationUser { UserName = "user", Email = "user@mail.com", PostLimitNumber = 10 };
				await userManager.CreateAsync(newAdmin, "P@ssw0rd");

				await userManager.AddToRoleAsync(newAdmin, "User");
			}
		}
	}
}
