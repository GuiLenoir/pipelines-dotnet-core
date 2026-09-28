using GaloRinha.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GaloRinha.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Galo> Galos => Set<Galo>();
}
