using Microsoft.EntityFrameworkCore;


namespace SocialMedia_BE.Models
{
    public class SocialMediaDBContext : DbContext
    {
        public SocialMediaDBContext(DbContextOptions<SocialMediaDBContext> options)
        : base(options)
        {
        }

        public DbSet<Post> TodoItems { get; set; } = null!;
    }
}
