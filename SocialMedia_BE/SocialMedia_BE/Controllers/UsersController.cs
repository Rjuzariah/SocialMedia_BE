﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.DbContexts;
using SocialMedia_BE.Models;
using System.Data;
using System.Security.Claims;

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
		private readonly SignInManager<ApplicationUser> _signInManager;


		public UsersController(ApplicationDBContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
		{
			_dbContext = dbContext;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;

		}

		// GET: api/<UsersController>
		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ApplicationUser>>> Get()
		{
			var users = await _dbContext.Users.ToListAsync();

			return Ok(users);
		}

		// GET api/<UsersController>/5
		[Authorize(Roles = "Admin")]
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(string id)
		{

			var user = await _userManager.FindByIdAsync(id);
			var existingRoles = await _userManager.GetRolesAsync(user);

			var userDto = new
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				PostLimitNumber = user.PostLimitNumber,
				Roles = existingRoles
			};

			return Ok(userDto);
		}

		[Authorize]
		[HttpGet("GetLoginUser")]
		public async Task<ActionResult> GetLoginUser()
		{
			ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;

			// Retrieve user ID from claims
			string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var user = await _userManager.FindByIdAsync(userId);

			if (user == null)
			{
				return Unauthorized();
			}

			var existingRoles = await _userManager.GetRolesAsync(user);

			var userDto = new
			{
				Id = user.Id,
				UserName = user.UserName,
				Roles = existingRoles
			};

			return Ok(userDto);
		}

		// POST api/<UsersController>
		[HttpPost]
		public async Task<ActionResult<ApplicationUser>> PostUser(ApplicationUserPostViewModel userCreate)
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

			//Assign user to roles
			await _userManager.AddToRolesAsync(user, userCreate.Roles);

			return CreatedAtAction(nameof(PostUser), new { id = user.Id }, user);
		}

		// PUT api/<UsersController>/5
		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
		public async Task<ActionResult<ApplicationUser>> Put(string id, ApplicationUserPutViewModel userUpdate)
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

			//Assign user to roles
			await _userManager.AddToRolesAsync(user, userUpdate.Roles);

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				// Handle update failure
				return BadRequest(result.Errors);
			}

			return NoContent();
		}

		[HttpPost("Logout")]
		[Authorize] // Ensure the user is authenticated
		public async Task<IActionResult> Logout()
		{
			// Perform any necessary cleanup or logout actions
			// For example, clearing authentication tokens or cookies

			await _signInManager.SignOutAsync();

			return Ok(new { message = "Logout successful" });

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
