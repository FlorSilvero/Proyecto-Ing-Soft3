using System.Text.Json;

public class CriticaFileService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IPeliculaService _peliculaService;
    private readonly string _filePath = "Data/Criticas.json";

    public CriticaFileService(IFileStorageService fileStorageService, IPeliculaService peliculaService)
    {
        _fileStorageService = fileStorageService;
        _peliculaService = peliculaService;
    }

    public Critica Create(Critica c)
    {
        List<Critica> criticas = (List<Critica>)GetAll();

        //Encontramos el maximo id existente
        int lastId = criticas.Max(c => c.Id);
        c.Id = lastId + 1;

        criticas.Add(c);
        var json = JsonSerializer.Serialize(criticas);
        _fileStorageService.Write(_filePath, json);
        return c;
    }

    public bool Delete(int id)
    {
      List<Critica> criticas = (List<Critica>)GetAll();
      Critica? criticaParaEliminar =  criticas.Find( c => c.Id == id);
      
      if( criticaParaEliminar is null ) return false;
      
      bool deleted = criticas.Remove(criticaParaEliminar) ;
      if ( deleted ) 
      {
        var json = JsonSerializer.Serialize(criticas);
        _fileStorageService.Write(_filePath , json);
      }
      return deleted;
    }

    public IEnumerable<Critica> GetAll()
    {
      var json = _fileStorageService.Read(_filePath);
      return JsonSerializer.Deserialize<List<Critica>>(json) ?? new();
    }

    public Critica? GetById(int id)
    {
      List<Critica> criticas = (List<Critica>)GetAll();
      return criticas.Find( c => c.Id == id);
    }

    public Boolean Update(int id, Critica c)
    {
      List<Critica> criticas = (List<Critica>)GetAll();
      int index = criticas.FindIndex( cr => cr.Id == id);
      //No se encontró el id que se quiere actualizar
      if ( index == -1 ) return false;

      criticas[index] = c;
      _fileStorageService.Write(_filePath, JsonSerializer.Serialize(criticas));
      return true;
    }
}