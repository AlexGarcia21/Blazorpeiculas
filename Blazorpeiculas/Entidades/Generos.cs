using Blazorpeiculas.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Blazorpeiculas.Entidades
{
    public class Generos
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        public string? Nombre { get; set; }
    }
}
