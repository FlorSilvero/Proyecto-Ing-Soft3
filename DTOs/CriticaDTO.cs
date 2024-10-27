using System.ComponentModel.DataAnnotations;

public class CriticaDTO {
    [Required(ErrorMessage = "El campo Descripcion es requerido")]
    public string? Descripcion { get; set; }
    [Range(1, 5, ErrorMessage = "El puntaje debe estar entre 1 y 5.")]
    public int Puntaje { get; set; }
    public int? PeliculaId { get; set; }
    public string? UsuarioId { get; set; }
}