using Blazorpeiculas.DTOs;

namespace Blazorpeiculas.Servicios;

public interface IServicioVoto
{
    Task Votar(VotoPeliculaDTO votoPeliculaDTO);
}
