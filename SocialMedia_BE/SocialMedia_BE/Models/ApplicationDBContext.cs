using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace SocialMedia_BE.Models
{
	public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }



		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	base.OnModelCreating(modelBuilder);

		//	var hasher = new PasswordHasher<IdentityUser>();


		//	//Seeding the User to AspNetUsers table
		//	modelBuilder.Entity<ApplicationUser>().HasData(
		//		new ApplicationUser
		//		{
		//			UserName = "Ria",
		//			Email = "r@r.com",
		//			PasswordHash = hasher.HashPassword(null, "P@ssw0rd1"),
		//			PostLimitNumber = 10
		//		}
		//	);

		//}

	}
}