using BlazorPeliculas.Entidades;

namespace Blazorpeiculas.DTOs;

public class HomeDTO
{
    public List <Pelicula>? EnCartelera { get; set; }
    public List<Pelicula>? FuturosEstrenos { get; set; }

}
