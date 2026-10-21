using Blazorpeiculas.Entidades;
using BlazorPeliculas.Entidades;

namespace Blazorpeiculas.DTOs;

public class PeliculaDetalleDTO
{
    public Pelicula Pelicula { get; set; } = null!;
    public List<Generos> Generos { get; set; } = [];
    public List<Actor> Actores { get; set; } = [];
    public int VotoUsuario { get; set; }
    public double PromedioVotos { get; set; }
}
