using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Repositories;

public class TryitterContext : DbContext
{
    public IConfiguration Configuration { get; }
    
    public TryitterContext(DbContextOptions<TryitterContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    public TryitterContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=master;User=SA;Password=Password12!;Encrypt=false;");
        // optionsBuilder.UseSqlServer(Configuration.GetConnectionString("sqlserver"));
        optionsBuilder.UseSqlite(Configuration.GetConnectionString("sqlite"));
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}