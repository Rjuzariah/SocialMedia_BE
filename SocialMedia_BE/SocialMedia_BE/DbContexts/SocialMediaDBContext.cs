using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.Models;


namespace SocialMedia_BE.DbContexts
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
