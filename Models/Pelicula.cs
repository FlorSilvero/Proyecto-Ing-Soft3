using System.Text.Json.Serialization;

public class Pelicula
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public string? FechaLanzamiento { get; set; }
    [JsonIgnore]
    public virtual List<Critica> Criticas { get; set; }

    public Pelicula()
    {

    }

    public Pelicula(int id, string titulo, string descripcion, string fechaLanzamiento)
    {
        Id = id;
        Titulo = titulo;
        Descripcion = descripcion;
        FechaLanzamiento = fechaLanzamiento;
    }

    public override string ToString()
    {
        return $"Id:{Id}, {Titulo}, {Descripcion}, {FechaLanzamiento}";
    }
}

