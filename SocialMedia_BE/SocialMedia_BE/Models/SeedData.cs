using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia_BE.Models
{
	public class SeedData
	{
		public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
		{
			using (var context = new ApplicationDBContext(
				serviceProvider.GetRequiredService<DbContextOptions<ApplicationDBContext>>()))
			{
				// Ensure database is created
				context.Database.EnsureCreated();

				// Seed initial data
				if (!context.Users.Any())
				{
					await SeedUsers(userManager);
				}
			}
		}

		private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
		{
			var user1 = new ApplicationUser { UserName = "Ria", Email = "r@r.com" };

			await userManager.CreateAsync(user1, "P@ssw0rd1");


		}
	}

}
