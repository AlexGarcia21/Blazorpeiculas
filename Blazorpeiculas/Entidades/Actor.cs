using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blazorpeiculas.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Blazorpeiculas.Entidades;

public class Actor
{
    public int Id { get; set; }
    [Required(ErrorMessage ="el campo {0} es requerido")]
    public string? Nombre { get; set; }
    public string? FotoURL { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public List<ActorPelicula> ActoresPeliculas { get; set; } = [];

    [NotMapped]
    public IBrowserFile? FotoArchivo { get; set; }
    [NotMapped]
    public string? Personaje { get; set; }
    [NotMapped]
    public ArchivoDTO? Archivo { get; set; }



}
