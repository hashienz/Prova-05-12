using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext
{
 
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    public DbSet <Pessoa> Pessoas {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=EnzoHashimoto.db");
        }
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {

    //     modelBuilder.Entity<Pessoa>().HasData(
    //         new Pessoa { Nome = "teste", DataCriacao = DateTime.Now },
    //         new Pessoa { Nome = "enzo", DataCriacao = DateTime.Now },
    //         new Pessoa { Nome = "fulano", DataCriacao = DateTime.Now }
    //     );

    // }
}