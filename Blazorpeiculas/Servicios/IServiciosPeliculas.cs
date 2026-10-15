using Blazorpeiculas.Entidades;
using BlazorPeliculas.Entidades;

namespace Blazorpeiculas.Servicios;

public interface IServicioPeliculas
{
    List<Pelicula> Obtenerpeliculas();
}