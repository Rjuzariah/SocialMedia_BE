using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.DbContexts;
using SocialMedia_BE.Models;

namespace SocialMedia_BE.Controllers
{
    [Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly SocialMediaDBContext _context;
        private readonly ApplicationDBContext _appContext;

		public PostsController(SocialMediaDBContext context, ApplicationDBContext appContext)
        {
            _context = context;
            _appContext = appContext;
        }

		// GET: api/Posts
		[HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
        {
            var postsWithOwnerNames = await _context.Posts
            .Include(p => p.Owner)
            .ToListAsync();

            //var postsWithOwnerNamesDto = postsWithOwnerNames
            //    .Select(p => new PostDto
            //    {
            //        Id = p.Id,
            //        Description = p.Description,
            //        OwnerName = p.Owner?.UserName,
            //        CreatedDateTime = p.CreatedDateTime
            //    })
            //    .ToList();

            foreach (var post in postsWithOwnerNames)
			{
				_context.Entry(post).Reference(p => p.Owner).Load();
			}

			var postsWithOwnerNamesDto = postsWithOwnerNames
				.Select(p => new PostDto
				{
					Id = p.Id,
					Description = p.Description,
					OwnerName = p.Owner?.UserName,
					CreatedDateTime = p.CreatedDateTime
				})
				.ToList();
			return postsWithOwnerNamesDto;

        }

		// GET: api/Posts/countNumberOfPost
		[HttpGet("CountNumberOfPost")]
		public async Task<ActionResult<int>> countNumberOfPost()
		{
			return await _context.Posts.CountAsync();
		}

		// GET: api/Posts/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        //// PUT: api/Posts/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPost(int id, Post post)
        //{
        //    if (id != post.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(post).State = EntityState.Modified;

        //    try
        //    {
        //        post.UpdatedDateTime = DateTime.Now;
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PostExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostDto>> PostPost([FromBody] CreatePost request)
        {
			ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;

			// Retrieve user ID from claims
			string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var userData = await _appContext.Users.FindAsync(userId);
            if (userData == null)
            {
                return Unauthorized();
			}
			var countNumberOfPostPerUser = _context.Posts.Where(x => x.OwnerId == userId).Count();

			if (userData?.PostLimitNumber > countNumberOfPostPerUser)
			{
                var postData = new Post();
				postData.Description = request.Description;
				postData.CreatedDateTime = DateTime.Now;
                postData.OwnerId = userData.Id;
                //postData.Owner = userData;

                _context.Posts.Add(postData);

				await _context.SaveChangesAsync();

                var returnData = new PostDto();
                returnData.Id = postData.Id;
                returnData.Description = postData.Description;
                returnData.CreatedDateTime = postData.CreatedDateTime;
                returnData.OwnerName = userData.UserName;

				return returnData;

			}
			else
			{
				return BadRequest(new { status = 400, title = "You already exceed the limit of posting number" });
			}


		}

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
