public class Critica
{
    public int Id { get; set; }
    public string? Descripcion { get; set; }
    public int Puntaje { get; set; }
    public Pelicula Pelicula { get; set; }
    public int? PeliculaId { get; set; }
    public ApplicationUser Usuario { get; set; }
    public string? UsuarioId { get; set; }
    
}