using System.ComponentModel.DataAnnotations;

public class PeliculaDTO {
    //puse que todos eran obligatorios pero ni idea
    [Required(ErrorMessage = "El campo Titulo es requerido")]
    public string? Titulo { get; set; }
    [Required(ErrorMessage = "El campo Descripcion es requerido")]
    public string? Descripcion { get; set; }
    [Required(ErrorMessage = "El campo FechaLanzamiento es requerido")]
    public string? FechaLanzamiento { get; set; }
}