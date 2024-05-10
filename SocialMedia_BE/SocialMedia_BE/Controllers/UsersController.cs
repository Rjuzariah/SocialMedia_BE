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

		private readonly ApplicationDBContext _dbContext;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;


		public UsersController(ApplicationDBContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_dbContext = dbContext;
			_userManager = userManager;
			_roleManager = roleManager;

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
		public async Task<ActionResult<ApplicationUser>> PostUser(ApplicationUserPostPutViewModel userCreate)
		{
			var user = new ApplicationUser();
			user.UserName = userCreate.UserName;
			user.Email = userCreate.Email;
			user.PostLimitNumber = userCreate.PostLimitNumber;

			var result = await _userManager.CreateAsync(user, userCreate.Password);
			if (!result.Succeeded)
			{
				// Handle update failure
				return BadRequest(result.Errors);
			}

			foreach (var roleId in userCreate.RoleIds)
			{
				var role = await _roleManager.FindByIdAsync(roleId);
				if (role == null)
				{
					// Handle role not found
					return BadRequest($"Role with ID '{roleId}' not found.");
				}

				await _userManager.AddToRoleAsync(user, role.Name);
			}


			//	_dbContext.Users.Add(user);
			//await _dbContext.SaveChangesAsync();

			return CreatedAtAction(nameof(PostUser), new { id = user.Id }, user);
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<ApplicationUser>> Put(string id, ApplicationUserPostPutViewModel userUpdate)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			// Update user data
			user.UserName = userUpdate.UserName;
			user.Email = userUpdate.Email;
			user.PostLimitNumber = userUpdate.PostLimitNumber;

			//change passowrd
			if (userUpdate.Password != "")
			{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var changePasswordResult = await _userManager.ResetPasswordAsync(user, token, userUpdate.Password);
				if (!changePasswordResult.Succeeded)
				{
					foreach (var error in changePasswordResult.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
					return BadRequest(ModelState);
				}
			}

			// Remove existing roles
			var existingRoles = await _userManager.GetRolesAsync(user);
			await _userManager.RemoveFromRolesAsync(user, existingRoles);

			// Add new roles
			foreach (var roleId in userUpdate.RoleIds)
			{
				var role = await _roleManager.FindByIdAsync(roleId);
				if (role == null)
				{
					// Handle role not found
					return BadRequest($"Role with ID '{roleId}' not found.");
				}

				await _userManager.AddToRoleAsync(user, role.Name);
			}

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				// Handle update failure
				return BadRequest(result.Errors);
			}

			return NoContent();
		}

		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}

		private bool DataExists(string id)
		{
			return _dbContext.Users.Any(e => e.Id == id);
		}
	}
}
