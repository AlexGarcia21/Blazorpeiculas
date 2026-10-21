using Blazorpeiculas.DTOs;
using Blazorpeiculas.Entidades;
using BlazorPeliculas.Entidades;

namespace Blazorpeiculas.Servicios;

public interface IServicioPeliculas
{
    Task<int> Crear(Pelicula pelicula);
    Task<HomeDTO> ObtenerPeliculasHome();
    Task<PeliculaDetalleDTO> ObtenerDetalle( int id);

}