using BlazorPeliculas.Entidades;

namespace Blazorpeiculas.Entidades;

public class VotoPelicula
{
    public int Id { get; set; }
    public int Voto { get; set; }
    public DateTime FechaVoto { get; set; }
    public int PeliculaId { get; set; }
    public Pelicula? Pelicula { get; set; }
    public string UsuarioId { get; set; } = null!;
}
