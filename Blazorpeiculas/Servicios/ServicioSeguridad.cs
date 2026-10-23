using Blazorpeiculas.Datos;
using Blazorpeiculas.DTOs;
using Blazorpeiculas.Entidades;
using Blazorpeiculas.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blazorpeiculas.Servicios;

public class ServicioSeguridad(IDbContextFactory<ApplicationDbContext> dbFactory, UserManager<ApplicationUser> userManager) : IServicioSeguridad
{
    public async Task<bool> HacerAdmin(string email)
    {
        using var context = dbFactory.CreateDbContext();
        var usuario = await userManager.FindByEmailAsync(email);

        if (usuario is null)
        {
            return false;
        }

        await userManager.AddToRoleAsync(usuario, "administrador");
        return true;

    }

    public async  Task<ResultadoPaginadoDTO<UsuarioDTO>> Obtener(PaginacionDTO paginacionDTO)
    {
        using var context = dbFactory.CreateDbContext();
        var elementos = await context.Users.OrderBy(x => x.UserName)
            .Paginar(paginacionDTO)
            .Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Email = u.Email!
            }).AsNoTracking().ToListAsync();

        var conteo = await context.Users.CountAsync();

        var respuesta = new ResultadoPaginadoDTO<UsuarioDTO>
        {
            CantidadTotalRegistros = conteo,
            Elementos = elementos
        };
        return respuesta;
    }

    public async Task<bool> RemoverAdmin(string email)
    {
        using var context = dbFactory.CreateDbContext();
        var usuario = await userManager.FindByEmailAsync(email);

        if (usuario is null)
        {
            return false;
        }

        await userManager.RemoveFromRoleAsync(usuario, "administrador");
        return true;
    }
}
