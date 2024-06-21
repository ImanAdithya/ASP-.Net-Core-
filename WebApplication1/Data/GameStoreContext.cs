using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options):DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id=1,Name="Fighting"},
            new {Id=2,Name="Sport"},
            new {Id=3,Name="Racing"},
            new {Id=4,Name="Comedy"}
            );
    }
}