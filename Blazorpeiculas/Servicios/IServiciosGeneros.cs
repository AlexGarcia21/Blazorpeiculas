using Blazorpeiculas.DTOs;
using Blazorpeiculas.Entidades;
namespace Blazorpeiculas.Servicios;

public interface IServiciosGeneros
{
    Task<int> Crear(Generos genero);
    Task<ResultadoPaginadoDTO<Generos>>Obtener(PaginacionDTO paginacionDTO);
    Task Actualizar (Generos genero);
    Task <Generos> ObtenerPorId(int id);
    Task <bool> Borrar(int id);

}
