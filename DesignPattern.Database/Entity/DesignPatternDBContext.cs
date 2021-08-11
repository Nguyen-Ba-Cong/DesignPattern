using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Database.Entity
{
    public class DesignPatternDBContext : DbContext
    {
        public DesignPatternDBContext()
        {

        }
        public DesignPatternDBContext(DbContextOptions<DesignPatternDBContext> options) : base(options) { }
        public DbSet<New> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasMany<New>(c => c.News)
                .WithMany(n => n.Categories);
            builder.Entity<New>()
                .HasOne(n => n.Users)
                .WithMany(c => c.News)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
