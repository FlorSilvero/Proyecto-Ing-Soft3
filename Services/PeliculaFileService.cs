using System.Text.Json;

public class PeliculaFileService
{
    private readonly string _filePath = "Data/Peliculas.json";
    private readonly IFileStorageService _fileStorageService;

    public PeliculaFileService(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public Pelicula Create(Pelicula p)
    {
        // Leer el contenido del archivo JSON
        var json = _fileStorageService.Read(_filePath);
        // Deserializar el JSON en una lista de peliculas
        var peliculas = JsonSerializer.Deserialize<List<Pelicula>>(json) ?? new List<Pelicula>();
        // Agregar la nueva pelicula a la lista
        peliculas.Add(p);
        // Serializar la lista actualizada de vuelta a JSON
        json = JsonSerializer.Serialize(peliculas);
        // Escribir el JSON actualizado en el archivo
        _fileStorageService.Write(_filePath, json);
        return p;
    }

    public void Delete(int id)
    {
        // Leer el contenido del archivo JSON
        var json = _fileStorageService.Read(_filePath);
        // Deserializar el JSON en una lista de peliculas
        var peliculas = JsonSerializer.Deserialize<List<Pelicula>>(json) ?? new List<Pelicula>();
        // Buscar la pelicula por id
        var pelicula = peliculas.Find(pelicula => pelicula.Id == id);

        // Si la pelicula existe, eliminarla de la lista
        if (pelicula is not null) 
        {
            peliculas.Remove(pelicula);
            // Serializar la lista actualizada de vuelta a JSON
            json = JsonSerializer.Serialize(peliculas);
            // Escribir el JSON actualizado en el archivo
            _fileStorageService.Write(_filePath, json);
        }
    }

    public IEnumerable<Pelicula> GetAll()
    {
        //Leo el contenido del archivo
        var json = _fileStorageService.Read(_filePath);
        //Deserializo el Json en una lista de Peliculas si es nulo retorna una lista vacia
        return JsonSerializer.Deserialize<List<Pelicula>>(json) ?? new List<Pelicula>();
    }

    public Pelicula? GetById(int id)
    {
        //Leo el contenido del archivo
        var json = _fileStorageService.Read(_filePath);
        //Deserializo el Json en una lista de Peliculas
        List<Pelicula>? peliculas = JsonSerializer.Deserialize<List<Pelicula>>(json);
        if(peliculas is null) return null;
        //Busco la pelicula por Id y devuelvo la pelicula encontrada
        return peliculas.Find(p => p.Id == id);  
    }

    public Pelicula? Update(int id, Pelicula pelicula)
    {
         // Leer el contenido del archivo JSON
        var json = _fileStorageService.Read(_filePath);
        // Deserializar el JSON en una lista de peliculas
        var peliculas = JsonSerializer.Deserialize<List<Pelicula>>(json) ?? new List<Pelicula>();
        // Buscar el índice de la pelicula por id
        var peliculaIndex = peliculas.FindIndex(p => p.Id == id);

        // Si la pelicula existe, reemplazarlo en la lista
        if (peliculaIndex >= 0) 
        {
            //reeplazo la pelicula de la lista por la pelicula recibida por parametro con los nuevos datos
            peliculas[peliculaIndex] = pelicula;
            // Serializar la lista actualizada de vuelta a JSON
            json = JsonSerializer.Serialize(peliculas);
            // Escribir el JSON actualizado en el archivo
            _fileStorageService.Write(_filePath, json);
            return pelicula;
        }

        // Retornar null si la pelicula no fue encontrada
        return null;
    }
}