using Blazorpeiculas.DTOs;
using Blazorpeiculas.Entidades;
namespace Blazorpeiculas.Servicios;

public interface IServiciosGeneros
{
    Task<int> Crear(Generos genero);
    Task<ResultadoPaginadoDTO<Generos>>Obtener(PaginacionDTO paginacionDTO);
}
