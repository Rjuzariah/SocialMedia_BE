namespace SocialMedia_BE.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string? Description { get; set; }
		public int Owner { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public DateTime? UpdatedDateTime { get; set; }
	}
}
