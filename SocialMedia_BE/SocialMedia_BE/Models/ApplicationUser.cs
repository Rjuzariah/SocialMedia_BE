using Microsoft.AspNetCore.Identity;

namespace SocialMedia_BE.Models
{
	public class ApplicationUser : IdentityUser
	{
        public int PostLimitNumber { get; set; }

		public virtual ICollection<IdentityRole> Roles { get; } = new List<IdentityRole>();
		public ICollection<Post> Posts { get; set; }

	}

	public class ApplicationUserPostViewModel
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public int PostLimitNumber { get; set; }
		public List<string> Roles { get; set; }


	}

	public class ApplicationUserPutViewModel
	{
		public string? Id { get; set; } = default!;
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public int PostLimitNumber { get; set; }
		public List<string> Roles { get; set; }


	}

	//public class MonthlyActiveUsersViewModel
	//{
	//	//public int Year { get; set; }
	//	//public int Month { get; set; }
	//	public int ActiveUsersCount { get; set; }

 //       public string UserId { get; set; }
 //       public string UserName { get; set; }
 //   }

}
