using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.DbContexts;
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
		public async Task<ActionResult<IEnumerable<ApplicationUser>>> Get()
		{
			var users = await _dbContext.Users.ToListAsync();

			return Ok(users);
		}

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
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				PostLimitNumber = user.PostLimitNumber,
				RoleIds = roleIds
			};

			return Ok(userDto);
		}

		// POST api/<UsersController>
		[HttpPost]
		public async Task<ActionResult<ApplicationUser>> PostUser(ApplicationUser user)
		{
			_dbContext.Users.Add(user);
			await _dbContext.SaveChangesAsync();

			return CreatedAtAction(nameof(PostUser), new { id = user.Id }, user);
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
