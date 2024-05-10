using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.DbContexts;
using SocialMedia_BE.Models;
using System;

namespace SocialMedia_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MAUController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;

		private readonly ApplicationDBContext _context;
		private readonly SignInManager<ApplicationUser> _signInManager;


		public MAUController(UserManager<ApplicationUser> userManager, ApplicationDBContext context ,SignInManager<ApplicationUser> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;

		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<IdentityUserLogin<string>>>> GetUserLogins()
		{
			var userLogins = await _context.UserLogins.ToListAsync();
			return userLogins;
		}

		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<MonthlyActiveUsersViewModel>>> GetUserLogins()
		//{
		//	var allUsers = await _userManager.Users.ToListAsync();

		//	var monthlyActiveUsers = new List<MonthlyActiveUsersViewModel>();
		//	foreach (var user in allUsers)
		//	{
		//		var logins = await _userManager.GetLoginsAsync(user);
		//		var activeUsersCount = logins.Count();
		//		var userViewModel = new MonthlyActiveUsersViewModel
		//		{
		//			UserId = user.Id,
		//			UserName = user.UserName,
		//			ActiveUsersCount = activeUsersCount
		//		};
		//		monthlyActiveUsers.Add(userViewModel);
		//	}

		//	return monthlyActiveUsers;

		//	//var allUsers = await _userManager.Users.ToListAsync();

		//	//var allLogins = new List<UserLoginInfo>();
		//	//foreach (var user in allUsers)
		//	//{
		//	//	var logins = await _userManager.GetLoginsAsync(user);
		//	//	allLogins.AddRange(logins);
		//	//}

		//	//return allLogins;

		//	////var userLogins = await _context.UserLogins.ToListAsync();
		//	////return userLogins;
		//}
	}
}
