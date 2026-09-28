using GaloRinha.Api;
using GaloRinha.Api.Data;
using GaloRinha.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=galos.db"));

builder.Services.AddScoped<GaloRepository>();

var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        mensagem = "GaloRinha API funcionando!"
    });
});

app.MapGet("/galos", async (GaloRepository repository) =>
{
    var galos = await repository.Listar();

    return Results.Ok(galos);
});

app.MapGet("/galos/{id:int}", async (int id, GaloRepository repository) =>
{
    var galo = await repository.Buscar(id);

    if (galo is null)
    {
        return Results.NotFound(new
        {
            mensagem = "Galo não encontrado."
        });
    }

    return Results.Ok(galo);
});

app.MapPost("/galos", async (Galo galo, GaloRepository repository) =>
{
    var novoGalo = await repository.Adicionar(galo);

    return Results.Created($"/galos/{novoGalo.Id}", novoGalo);
});

app.MapPut("/galos/{id:int}", async (
    int id,
    Galo dados,
    GaloRepository repository) =>
{
    var atualizado = await repository.Atualizar(id, dados);

    if (!atualizado)
    {
        return Results.NotFound(new
        {
            mensagem = "Galo não encontrado."
        });
    }

    var galo = await repository.Buscar(id);

    return Results.Ok(galo);
});

app.MapDelete("/galos/{id:int}", async (
    int id,
    GaloRepository repository) =>
{
    var removido = await repository.Remover(id);

    if (!removido)
    {
        return Results.NotFound(new
        {
            mensagem = "Galo não encontrado."
        });
    }

    return Results.NoContent();
});

app.Run();
