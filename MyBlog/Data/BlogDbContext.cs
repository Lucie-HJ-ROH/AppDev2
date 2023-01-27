using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Model;

namespace MyBlog.Data;

public class BlogDbContext : IdentityDbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) {}

    public DbSet<Article> Articles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data source=Articles.db");
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    // }
}