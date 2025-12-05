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

// --- ENDPOINTS DE TAREFA ---

// GET: Listar todas
app.MapGet("/api/tarefas/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Tarefas.Any())
    {
        return Results.Ok(ctx.Tarefas.Include(t => t.Categoria).ToList());
    }
    return Results.NotFound("Nenhuma tarefa encontrada");
});

// POST: Cadastrar
app.MapPost("/api/tarefas/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Tarefa tarefa) =>
{
    // FORÇAR O STATUS INICIAL
    tarefa.Status = "Não iniciada";
    
    ctx.Tarefas.Add(tarefa);
    ctx.SaveChanges();
    return Results.Created("", tarefa);
});

// PATCH: Alterar Status (A Lógica de Ouro da Prova)
// Não precisamos receber nada no corpo, só o ID na URL
app.MapPatch("/api/tarefas/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string id) =>
{
    Tarefa? tarefa = ctx.Tarefas.Find(id);

    if (tarefa is null)
    {
        return Results.NotFound("Tarefa não encontrada");
    }

    // LÓGICA DE PROGRESSÃO DE STATUS
    // Se for "Chamado", mude para: "Aberto" -> "Em atendimento" -> "Resolvido"
    switch (tarefa.Status)
    {
        case "Não iniciada":
            tarefa.Status = "Em andamento";
            break;
        case "Em andamento":
            tarefa.Status = "Concluída";
            break;
        case "Concluída":
            // Já está no final, não faz nada
            break;
        default:
            tarefa.Status = "Não iniciada"; // Fallback caso venha nulo
            break;
    }

    ctx.Tarefas.Update(tarefa);
    ctx.SaveChanges();
    return Results.Ok(tarefa);
});

// GET: Não Concluídas
app.MapGet("/api/tarefas/naoconcluidas", ([FromServices] AppDataContext ctx) =>
{
    var tarefas = ctx.Tarefas
                     .Include(t => t.Categoria)
                     .Where(t => t.Status == "Não iniciada" || t.Status == "Em andamento")
                     .ToList();
    return Results.Ok(tarefas);
});

// GET: Concluídas (Apenas Concluída)
app.MapGet("/api/tarefas/concluidas", ([FromServices] AppDataContext ctx) =>
{
    var tarefas = ctx.Tarefas
                     .Include(t => t.Categoria)
                     .Where(t => t.Status == "Concluída")
                     .ToList();
    return Results.Ok(tarefas);
});
// --- ENDPOINT DE CATEGORIA (Para preencher o <select>) ---
app.MapPost("/api/categoria/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Categoria categoria) =>
{
    ctx.Categorias.Add(categoria);
    ctx.SaveChanges();
    return Results.Created("", categoria);
});

app.MapGet("/api/categoria/listar", ([FromServices] AppDataContext ctx) =>
{
    return Results.Ok(ctx.Categorias.ToList());
});


// --- ENDPOINTS DE FOLHA DE PAGAMENTO ---

// POST: Cadastrar com Cálculos e Validação
app.MapPost("/api/folha/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Folha folha) =>
{
    // 1. VALIDAÇÃO DE DUPLICIDADE (O CPF já tem folha nesse mês/ano?)
    bool existe = ctx.Folhas.Any(f => f.Cpf == folha.Cpf && f.Mes == folha.Mes && f.Ano == folha.Ano);
    if (existe)
    {
        return Results.BadRequest("Já existe uma folha cadastrada para este CPF neste período!");
    }

    // 2. CÁLCULOS MATEMÁTICOS (Regra de Negócio)
    folha.SalarioBruto = folha.HorasTrabalhadas * folha.ValorHora;

    // Cálculo simples de IR (Exemplo: 20% se ganhar mais de 2000)
    if (folha.SalarioBruto > 2000)
        folha.ImpostoRenda = folha.SalarioBruto * 0.20;
    else
        folha.ImpostoRenda = 0;

    // Cálculo simples de INSS (Exemplo fixo de 8%)
    folha.Inss = folha.SalarioBruto * 0.08;

    folha.SalarioLiquido = folha.SalarioBruto - folha.ImpostoRenda - folha.Inss;

    // 3. SALVAR
    ctx.Folhas.Add(folha);
    ctx.SaveChanges();
    return Results.Created("", folha);
});

// GET: Listar Todas
app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) =>
{
    return Results.Ok(ctx.Folhas.ToList());
});

// GET: Buscar Específica (Busca Composta)
app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", ([FromServices] AppDataContext ctx, 
    string cpf, int mes, int ano) =>
{
    var folha = ctx.Folhas.FirstOrDefault(f => f.Cpf == cpf && f.Mes == mes && f.Ano == ano);
    if (folha is null) return Results.NotFound();
    return Results.Ok(folha);
});
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