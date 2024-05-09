using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMedia_BE.Controllers

{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;


		private readonly ApplicationDBContext _dbContext;

		public UsersController(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		// GET: api/<UsersController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<IdentityUser>>> Get()
		{
			var users = await _dbContext.Users.ToListAsync();

			return Ok(users);
		}

		//[HttpGet("{userId}")]
		//public async Task<IActionResult> GetUser(string userId)
		//{
		//	var user = await _dbContext.Users.FindAsync(userId);
		//	if (user == null)
		//	{
		//		return NotFound();
		//	}

		//	var roles = await _dbContext.(user);

		//	var userDto = new
		//	{
		//		UserId = user.Id,
		//		UserName = user.UserName,
		//		Email = user.Email,
		//		Roles = roles // Include roles in the response
		//	};

		//	return Ok(userDto);
		//}

		// GET api/<UsersController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(string id)
		{
			var user = await _dbContext.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			// Query role IDs associated with the user directly from DbContext
			var roleIds = await _dbContext.UserRoles
				.Where(ur => ur.UserId == id)
				.Select(ur => ur.RoleId)
				.ToListAsync();

			var userDto = new
			{
				UserId = user.Id,
				UserName = user.UserName,
				// Include other user properties as needed
				RoleIds = roleIds
			};


			//var user = await _dbContext.Users.FindAsync(id);
			//var user1 = await _userManager.FindByIdAsync(id);

			//var roles = await _userManager.GetRolesAsync(user1);

			//var roles = await _dbContext.UserRoles
			//         .Where(ur => ur.UserId == id)
			//         .Select(ur => ur.RoleId)
			//         .ToListAsync();

			//user.UserRoles = roles;

			return Ok(userDto);
		}

		// POST api/<UsersController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
