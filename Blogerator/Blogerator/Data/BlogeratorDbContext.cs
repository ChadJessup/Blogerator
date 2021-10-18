#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using Microsoft.EntityFrameworkCore;

namespace Blogerator.Data;

public class BlogeratorDbContext : DbContext
{
    private readonly IConfiguration configuration;

    public BlogeratorDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(this.configuration.GetConnectionString("Adhdev"));
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
