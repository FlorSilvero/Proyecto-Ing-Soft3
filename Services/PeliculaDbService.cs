
using Microsoft.EntityFrameworkCore;

public class PeliculaDbService : IPeliculaService
{
    private readonly PeliculasContext _context;

    public PeliculaDbService(PeliculasContext context)
    {
        _context = context;
    }

    public Pelicula Create(PeliculaDTO p)
    {
        Pelicula pelicula = new()
        {
            Titulo = p.Titulo,
            Descripcion = p.Descripcion,
            FechaLanzamiento = p.FechaLanzamiento
        };
        _context.Peliculas.Add(pelicula);
        _context.SaveChanges();
        return pelicula;
    }

    public void Delete(int id)
    {
        var p = _context.Peliculas.Find(id);
        _context.Peliculas.Remove(p);
        _context.SaveChanges();
    }

    public IEnumerable<Pelicula> GetAll()
    {
        return _context.Peliculas;
    }

    public Pelicula? GetById(int id)
    {
        return _context.Peliculas.Find(id);
    }

    public IEnumerable<Critica> GetCriticas(int id)
    {
        Pelicula p = _context.Peliculas.Include(p => p.Criticas).FirstOrDefault(x => x.Id == id);
        return p.Criticas;
    }

    public Pelicula? Update(int id, Pelicula p)
    {
        _context.Entry(p).State = EntityState.Modified;
        _context.SaveChanges();
        return p;
    }
}
