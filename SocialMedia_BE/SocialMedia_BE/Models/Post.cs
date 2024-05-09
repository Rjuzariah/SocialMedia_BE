using System.ComponentModel.DataAnnotations;

namespace SocialMedia_BE.Models
{
	public class Post
	{
		[Key]
		public int Id { get; set; }
		public string? Description { get; set; }
		public string? OwnerId { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public DateTime? UpdatedDateTime { get; set; }
	}
}
