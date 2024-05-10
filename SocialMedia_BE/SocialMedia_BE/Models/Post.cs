using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia_BE.Models
{
	public class Post
	{
		[Key]
		public int Id { get; set; }
		public string? Description { get; set; }

		public string? OwnerId { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public ApplicationUser Owner { get; set; }
	}

	public class PostDto
	{
		public int Id { get; set; }
		public string? Description { get; set; }
		public string? OwnerName { get; set; }
		public DateTime CreatedDateTime { get; set; }
	}

	public class CreatePost
	{
		public string? Description { get; set; }
	}
}
