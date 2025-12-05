using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total",
        configs => configs
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
);

var app = builder.Build();

app.MapGet("/", () => "Enzo Ricardo Hashimoto");

// --- ENDPOINTS DE PESSOA ---

// GET: Listar todas
app.MapGet("/api/pessoas/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Pessoas.Any())
    {
        return Results.Ok(ctx.Pessoas);
    }
    return Results.NotFound("Nenhuma pessoa encontrada");
});

// // POST: Cadastrar
// app.MapPost("/api/pessoas/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Pessoa pessoa) =>
// {
//     ctx.Pessoa.Add(pessoa);
//     ctx.SaveChanges();
//     return Results.Created("", pessoa);
// });

// PATCH: Alterar Status 
// Não precisamos receber nada no corpo, só o ID na URL
app.MapPatch("/api/pessoas/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string PessoaId) =>
{
    Pessoa? pessoa = ctx.Pessoas.Find(PessoaId);

    if (pessoa is null)
    {
        return Results.NotFound("Pessoa não encontrada");
    }

    ctx.Pessoas.Update(pessoa);
    ctx.SaveChanges();
    return Results.Ok(pessoa);
});


// // --- ENDPOINT DE CATEGORIA (Para preencher o <select>) ---
// app.MapPost("/api/categoria/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Categoria categoria) =>
// {
//     ctx.Categorias.Add(categoria);
//     ctx.SaveChanges();
//     return Results.Created("", categoria);
// });

// app.MapGet("/api/categoria/listar", ([FromServices] AppDataContext ctx) =>
// {
//     return Results.Ok(ctx.Categorias.ToList());
// });



// POST:
app.MapPost("/api/pessoa/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Pessoa pessoa) =>
{
    pessoa.Imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura );
    
    if (pessoa.Imc < 18.5)
        pessoa.Classificacao = "Magreza";
        if (pessoa.Imc > 18.5 && pessoa.Imc < 24.9)
        pessoa.Classificacao = "Normal";
        if (pessoa.Imc >= 25 && pessoa.Imc <= 29.9)
        pessoa.Classificacao = "Sobrepeso";
        if (pessoa.Imc < 18.5)
        pessoa.Classificacao = "Obesidade I";
        if (pessoa.Imc < 18.5)
        pessoa.Classificacao = "Obesidade II";
        if (pessoa.Imc > 40)
        pessoa.Classificacao = "Obesidade  III";

    
    // 3. SALVAR
    ctx.Pessoas.Add(pessoa);
    ctx.SaveChanges();
    return Results.Created("", pessoa);
});

// // GET: Listar Todas
// app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) =>
// {
//     return Results.Ok(ctx.Folhas.ToList());
// });

// // GET: Buscar Específica (Busca Composta)
// app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", ([FromServices] AppDataContext ctx, 
//     string cpf, int mes, int ano) =>
// {
//     var folha = ctx.Folhas.FirstOrDefault(f => f.Cpf == cpf && f.Mes == mes && f.Ano == ano);
//     if (folha is null) return Results.NotFound();
//     return Results.Ok(folha);
// });
app.UseCors("Acesso Total");

app.Run();


/* =============================================================================

1. SE PEDIR ORDENAÇÃO (Ex: Listar produtos do mais caro para o mais barato):
   return Results.Ok(ctx.Produtos.OrderByDescending(p => p.Preco).ToList());

2. SE PEDIR BUSCA POR PARTE DO NOME (Ex: Buscar quem tem "Silva" no nome):
   app.MapGet("/api/buscar/{nome}", (AppDataContext ctx, string nome) => {
       return Results.Ok(ctx.Tabela.Where(x => x.Nome.Contains(nome)).ToList());
   });

3. SE PEDIR SOMA TOTAL (Ex: Total de todos os salários):
   app.MapGet("/api/total", (AppDataContext ctx) => {
       double total = ctx.Folhas.Sum(f => f.SalarioLiquido);
       return Results.Ok(total);
   });
   
4. SE PEDIR MÉDIA:
   double media = ctx.Folhas.Average(f => f.SalarioLiquido);
*/