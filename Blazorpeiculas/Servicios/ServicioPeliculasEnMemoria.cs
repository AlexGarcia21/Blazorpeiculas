using Blazorpeiculas.Entidades;
using BlazorPeliculas.Entidades;

namespace Blazorpeiculas.Servicios;

public class ServicioPeliculasEnMemoria : IServicioPeliculas
{
    public List<Pelicula> Obtenerpeliculas()
    {
        return new List<Pelicula>
        {
            new Pelicula
            {
                Id = 1,
                Titulo = "capitan america",
                FechaLanzamiento = new DateTime(2025, 2, 14),
                PosterURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSYya5aVfUjA1Z9UL6rbwp6OOQQF8QTNPPnYjUox5kmog&s=10"
            },
            new Pelicula
            {
                Id = 2,
                Titulo = "huevos",
                FechaLanzamiento = new DateTime(2025, 5, 23),
                PosterURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSemNgwo-NO_qvifwFvbwL3I8VfG6j7anA3nCJYvT8tqw&s=10"
            }
        };
    }
}