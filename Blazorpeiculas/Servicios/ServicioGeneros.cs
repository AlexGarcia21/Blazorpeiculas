using Blazorpeiculas.Datos;
using Blazorpeiculas.DTOs;
using Blazorpeiculas.Entidades;
using Blazorpeiculas.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace Blazorpeiculas.Servicios;

public class ServicioGeneros(IDbContextFactory<ApplicationDbContext> dbFactory) : IServiciosGeneros
{
    public async Task Actualizar(Generos genero)
    {
         using var context = dbFactory.CreateDbContext();
        context.Update(genero);
        await context.SaveChangesAsync();
    }

    public async Task<bool> Borrar(int id)
    {
        using var context = dbFactory.CreateDbContext();
        var elementosBorrados= await context.Generos.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        return elementosBorrados == 1;
    }

    public async Task<int> Crear(Generos genero)
    {
        using var context = dbFactory.CreateDbContext();
        context.Add(genero);
        await context.SaveChangesAsync();
        return genero.Id;
    }

    public async  Task<ResultadoPaginadoDTO<Generos>> Obtener(PaginacionDTO paginacionDTO)
    {
        using var context = dbFactory.CreateDbContext();
        var elementos = await context.Generos.OrderBy(g => g.Nombre)
            .Paginar(paginacionDTO)
            .AsNoTracking().ToListAsync();
        var conteo = await context.Generos.CountAsync();
        var respuesta = new ResultadoPaginadoDTO<Generos>
        {
            CantidadTotalRegistros = conteo,
            Elementos = elementos
           
        };


        var cantidadTotalRegistros = await context.Generos.CountAsync();
        return new ResultadoPaginadoDTO<Generos>
        {
            Elementos = elementos,
            CantidadTotalRegistros = cantidadTotalRegistros
        };
    }

    public async Task<Generos> ObtenerPorId(int id)
    {
        using var context = dbFactory.CreateDbContext();
        return await context.Generos.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Generos>> ObtenerTodos()
    {
        using var context = dbFactory.CreateDbContext();
        return await context.Generos.OrderBy(x => x.Nombre).ToListAsync();

    }
}
