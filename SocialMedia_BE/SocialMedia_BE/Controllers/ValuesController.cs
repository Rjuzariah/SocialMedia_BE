using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetString()
		{
			return Ok("Hello, this is your string response from the API!");
		}
	}
}
