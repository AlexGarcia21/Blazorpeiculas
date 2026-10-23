using Blazorpeiculas.Entidades;
using BlazorPeliculas.Entidades;

namespace Blazorpeiculas.DTOs;

    public record EditarPeliculaDTO(Pelicula Pelicula, List<Actor> Actores,
        List<Generos> GenerosSeleccionados,
        List<Generos> GenerosNoSeleccionados);

