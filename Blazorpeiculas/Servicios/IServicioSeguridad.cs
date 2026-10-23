using Blazorpeiculas.DTOs;

namespace Blazorpeiculas.Servicios
{
    public interface IServicioSeguridad
    {
        Task<ResultadoPaginadoDTO<UsuarioDTO>> Obtener(PaginacionDTO paginacionDTO);
        Task<bool> HacerAdmin(string email);
        Task<bool> RemoverAdmin(string email);

    }
}
