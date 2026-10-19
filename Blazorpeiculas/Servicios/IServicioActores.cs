using Blazorpeiculas.Entidades;

namespace Blazorpeiculas.Servicios;

public interface IServicioActores
{
    Task<int> Crear(Actor actor);
}
