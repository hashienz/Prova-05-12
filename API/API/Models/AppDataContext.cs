using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext
{
 
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Folha> Folhas { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=EnzoHashimoto.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { CategoriaId = 1, Nome = "Trabalho", CriadoEm = DateTime.Now },
            new Categoria { CategoriaId = 2, Nome = "Estudos", CriadoEm = DateTime.Now },
            new Categoria { CategoriaId = 3, Nome = "Lazer", CriadoEm = DateTime.Now }
        );

        modelBuilder.Entity<Tarefa>().HasData(
            new Tarefa { 
                TarefaId = "6a8b3e4d-5e4e-4f7e-bdc9-9181e456ad0e", 
                Titulo = "Concluir relatório", 
                CriadoEm = DateTime.Now.AddDays(7), 
                Status = "Não iniciada",
                CategoriaId = 1 
            },
            new Tarefa { 
                TarefaId = "2f1b7dc1-3b9a-4e1a-a389-7f5d2f1c8f3e", 
                Titulo = "Estudar Angular", 
                CriadoEm = DateTime.Now.AddDays(3), 
                Status = "Não iniciada",
                CategoriaId = 2 
            },
            new Tarefa { 
                TarefaId = "e5d4a7b9-1f9e-4c4a-ae3b-5b7c1a9d2e3f", 
                Titulo = "Passeio no parque", 
                CriadoEm = DateTime.Now.AddDays(14), 
                Status = "Não iniciada",
                CategoriaId = 3 
            }
        );
    }
}