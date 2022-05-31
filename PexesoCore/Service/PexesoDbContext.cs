using Microsoft.EntityFrameworkCore;
using PexesoCore.Entity;

namespace PexesoCore.Service;


public class PexesoDbContext : DbContext
{
    public DbSet<Time> times { get; set; }
    public DbSet<Comment> comments { get; set; }
    public DbSet<Rating> ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Pexeso;Username=postgres;Password=root");
}    