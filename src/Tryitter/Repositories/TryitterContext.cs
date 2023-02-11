using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Repositories;

public class TryitterContext : DbContext
{

    public TryitterContext(DbContextOptions<TryitterContext> options) : base(options)
    {
    }

    public TryitterContext()
    {
        
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=tryitter3;User=SA;Password=Password12!;Encrypt=false;");
    // }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}