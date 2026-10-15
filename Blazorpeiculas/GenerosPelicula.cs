using BlazorPeliculas.Entidades;

namespace Blazorpeiculas.Entidades;

public class GeneroPelicula
{
    public int PeliculaId { get; set; }
    public int GeneroId { get; set; }
    public Pelicula? Pelicula { get; set; }
    public Generos? Genero { get; set; }
}