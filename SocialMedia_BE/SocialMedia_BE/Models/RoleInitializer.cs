using Microsoft.AspNetCore.Identity;

namespace SocialMedia_BE.Models
{
	public class RoleInitializer
	{
		public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
		{
			if (!await roleManager.RoleExistsAsync("Admin"))
			{
				await roleManager.CreateAsync(new IdentityRole("Admin"));
			}

			if (!await roleManager.RoleExistsAsync("User"))
			{
				await roleManager.CreateAsync(new IdentityRole("User"));
			}
		}
	}
}
