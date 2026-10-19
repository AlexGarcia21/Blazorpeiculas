using Blazorpeiculas.Datos;
using Blazorpeiculas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Blazorpeiculas.Servicios;

public class ServicioActores(IDbContextFactory<ApplicationDbContext> dbFactory) : IServicioActores
{
    public async Task<int> Crear(Actor actor)
    {
        using var context = dbFactory.CreateDbContext();
        context.Add(actor);
        await context.SaveChangesAsync();
        return actor.Id;
    }
}
