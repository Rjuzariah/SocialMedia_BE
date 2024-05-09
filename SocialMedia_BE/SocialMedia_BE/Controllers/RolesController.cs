using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.DbContexts;

namespace SocialMedia_BE.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly ApplicationDBContext _dbContext;

		public RolesController(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		// GET: api/<RolesController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<IdentityRole>>> Get()
		{
			var users = await _dbContext.Roles.ToListAsync();

			return Ok(users);
		}
	}
}
