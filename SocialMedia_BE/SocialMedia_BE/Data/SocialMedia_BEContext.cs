using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.Models;

namespace SocialMedia_BE.Data
{
    public class SocialMedia_BEContext : DbContext
    {
        public SocialMedia_BEContext (DbContextOptions<SocialMedia_BEContext> options)
            : base(options)
        {
        }

        public DbSet<SocialMedia_BE.Models.Post> Post { get; set; } = default!;
    }
}
