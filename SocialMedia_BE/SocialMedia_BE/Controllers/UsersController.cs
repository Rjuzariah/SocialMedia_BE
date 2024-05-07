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

		// GET api/<UsersController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
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
