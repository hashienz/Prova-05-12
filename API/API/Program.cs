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
app.MapGet("/api/pessoa/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Pessoas.Any())
    {
        return Results.Ok(ctx.Pessoas.ToList());
    }
    return Results.NotFound("Nenhuma pessoa encontrada");
});

// POST: Cadastrar
app.MapPost("/api/pessoa/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Pessoa pessoa) =>
{
    pessoa.Imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura );
    
    if (pessoa.Imc < 18.5)
        pessoa.Classificacao = "Magreza";
        if (pessoa.Imc > 18.5 && pessoa.Imc < 24.9)
        pessoa.Classificacao = "Normal";
        if (pessoa.Imc >= 25 && pessoa.Imc <= 29.9)
        pessoa.Classificacao = "Sobrepeso";
        if (pessoa.Imc >= 30 && pessoa.Imc <=34.9)
        pessoa.Classificacao = "Obesidade I";
        if (pessoa.Imc >= 35 && pessoa.Imc <=39.9)
        pessoa.Classificacao = "Obesidade II";
        if (pessoa.Imc > 40)
        pessoa.Classificacao = "Obesidade  III";

    ctx.Pessoas.Add(pessoa);
    ctx.SaveChanges();
    return Results.Created("", pessoa);
});
// PATCH: Alterar
app.MapPatch("/api/pessoa/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string id, [FromBody] Pessoa pessoaAtualizada) =>
{
    var pessoa = ctx.Pessoas.Find(id);

    if (pessoa is null)
    {
        return Results.NotFound("Pessoa não encontrada");
    }

    if (pessoaAtualizada.Nome != null)
        pessoa.Nome = pessoaAtualizada.Nome;
    if (pessoaAtualizada.Altura != 0)
        pessoa.Altura = pessoaAtualizada.Altura;
    if (pessoaAtualizada.Peso != 0)
        pessoa.Peso = pessoaAtualizada.Peso;

    pessoa.Imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);
    if (pessoa.Imc < 18.5)
        pessoa.Classificacao = "Magreza";
    else if (pessoa.Imc >= 18.5 && pessoa.Imc < 24.9)
        pessoa.Classificacao = "Normal";
    else if (pessoa.Imc >= 25 && pessoa.Imc <= 29.9)
        pessoa.Classificacao = "Sobrepeso";
    else if (pessoa.Imc >= 30 && pessoa.Imc <= 34.9)
        pessoa.Classificacao = "Obesidade I";
    else if (pessoa.Imc >= 35 && pessoa.Imc <= 39.9)
        pessoa.Classificacao = "Obesidade II";
    else if (pessoa.Imc >= 40)
        pessoa.Classificacao = "Obesidade III";

    ctx.Pessoas.Update(pessoa);
    ctx.SaveChanges();
    return Results.Ok(pessoa);
});


// app.MapPut("/api/pessoa/filtrar/{classificacao}", ([FromServices] AppDataContext ctx, [FromRoute] string id, [FromBody] Pessoa pessoaAtualizada) =>
// {
//     var pessoa = ctx.Pessoas.Find(id);

//     if (pessoa is null)
//     {
//         return Results.NotFound("Pessoa não encontrada");
//     }


//     pessoa.Nome = pessoaAtualizada.Nome;
//     pessoa.Altura = pessoaAtualizada.Altura;
//     pessoa.Peso = pessoaAtualizada.Peso;

//     pessoa.Imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);
//     if (pessoa.Imc < 18.5)
//         pessoa.Classificacao = "Magreza";
//     else if (pessoa.Imc >= 18.5 && pessoa.Imc < 24.9)
//         pessoa.Classificacao = "Normal";
//     else if (pessoa.Imc >= 25 && pessoa.Imc <= 29.9)
//         pessoa.Classificacao = "Sobrepeso";
//     else if (pessoa.Imc >= 30 && pessoa.Imc <= 34.9)
//         pessoa.Classificacao = "Obesidade I";
//     else if (pessoa.Imc >= 35 && pessoa.Imc <= 39.9)
//         pessoa.Classificacao = "Obesidade II";
//     else if (pessoa.Imc >= 40)
//         pessoa.Classificacao = "Obesidade III";


//     ctx.Pessoas.Update(pessoa);
//     ctx.SaveChanges();
//     return Results.Ok(pessoa);
// });



app.UseCors("Acesso Total");

app.Run();