using Blazorpeiculas.Datos;
using Blazorpeiculas.DTOs;
using Blazorpeiculas.Entidades;
using BlazorPeliculas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Blazorpeiculas.Servicios;

public class ServicioPeliculas(IDbContextFactory<ApplicationDbContext> dbFactory,
    IAlmacenadorArchivos almacenadorArchivos) : IServicioPeliculas
{
    private readonly string contenedor = "peliculas";

    public async Task<int> Crear(Pelicula pelicula)
    {
        if (pelicula is not null)
        {
            pelicula.PosterURL = await almacenadorArchivos.Almacenar(contenedor, pelicula.Archivo);
        }

        using var context = dbFactory.CreateDbContext();
        context.Add(pelicula);
        await context.SaveChangesAsync();
        return pelicula.Id;
    }

    public async Task<PeliculaDetalleDTO> ObtenerDetalle(int id)
    {
        using var context = dbFactory.CreateDbContext();
        var pelicula = await context.Peliculas.Where(p => p.Id == id)
            .Include(p => p.GenerosPeliculas)
                .ThenInclude(gp => gp.Genero)
            .Include(p => p.ActoresPeliculas.OrderBy(pa => pa.Orden))
                .ThenInclude(pa => pa.Actor)
            .FirstOrDefaultAsync();

        if (pelicula is null)
        {
            return null;
        }
        var promedioVoto = 4;
        var votoUsuario = 5;

        var modelo = new PeliculaDetalleDTO();
        modelo.Pelicula = pelicula;
        modelo.Generos = pelicula.GenerosPeliculas.Select(gp => gp.Genero!).ToList();
        modelo.Actores = pelicula.ActoresPeliculas.Select(pa => new Actor
        {
            Id = pa.ActorId,
            Nombre = pa.Actor!.Nombre,
            Personaje = pa.Personaje,
            FotoURL = pa.Actor.FotoURL
        }).ToList();

        modelo.PromedioVotos = promedioVoto;
        modelo.VotoUsuario = votoUsuario;

        return modelo;
    }

    public async Task<HomeDTO> ObtenerPeliculasHome()
    {
        var limite = 5;
        using var context = dbFactory.CreateDbContext();

        var cartelera = await context.Peliculas
            .Where(x => x.EnCartelera)
            .OrderBy(x => x.FechaLanzamiento)
            .Take(limite).ToListAsync();

        var hoy = DateTime.Today;

        var futurosEstrenos = await context.Peliculas
    .Where(x => x.FechaLanzamiento > hoy)
    .OrderBy(x => x.FechaLanzamiento)
    .Take(limite).ToListAsync();

        var respuesta = new HomeDTO
        {
            EnCartelera = cartelera,
            FuturosEstrenos = futurosEstrenos
        };

        return respuesta;
    }
}
