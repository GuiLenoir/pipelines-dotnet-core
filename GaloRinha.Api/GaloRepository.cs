using GaloRinha.Api.Data;
using GaloRinha.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GaloRinha.Api;

public class GaloRepository
{
    private readonly AppDbContext _db;

    public GaloRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Galo>> Listar()
    {
        return await _db.Galos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Galo?> Buscar(int id)
    {
        return await _db.Galos
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Galo> Adicionar(Galo galo)
    {
        _db.Galos.Add(galo);
        await _db.SaveChangesAsync();

        return galo;
    }

    public async Task<bool> Atualizar(int id, Galo dados)
    {
        var galo = await _db.Galos.FindAsync(id);

        if (galo is null)
            return false;

        galo.Nome = dados.Nome;
        galo.Vitorias = dados.Vitorias;
        galo.Derrotas = dados.Derrotas;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Remover(int id)
    {
        var galo = await _db.Galos.FindAsync(id);

        if (galo is null)
            return false;

        _db.Galos.Remove(galo);
        await _db.SaveChangesAsync();

        return true;
    }
}
