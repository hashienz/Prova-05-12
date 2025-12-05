using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext
{
 
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas {get; set;}

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=EnzoHashimoto.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Pessoa>().HasData(
            new Pessoa { Nome = "teste", DataCriacao = DateTime.Now },
            new Pessoa { Nome = "enzo", DataCriacao = DateTime.Now },
            new Pessoa { Nome = "fulano", DataCriacao = DateTime.Now }
        );

        modelBuilder.Entity<Pessoa>().HasData(
            new Pessoa { 
                PessoaId = "6a8b3e4d-5e4e-4f7e-bdc9-9181e456ad0e", 
                Nome = "teste", 
                DataCriacao = DateTime.Now.AddDays(7)
            },
            new Pessoa { 
                PessoaId = "2f1b7dc1-3b9a-4e1a-a389-7f5d2f1c8f3e", 
                Nome = "enzo", 
                DataCriacao = DateTime.Now.AddDays(7)
            },
            new Pessoa { 
                PessoaId = "e5d4a7b9-1f9e-4c4a-ae3b-5b7c1a9d2e3f", 
                Nome = "fulano", 
                DataCriacao = DateTime.Now.AddDays(7)
            }
        );
    }
}