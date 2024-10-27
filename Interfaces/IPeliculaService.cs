public interface IPeliculaService
{
    public IEnumerable<Pelicula> GetAll();

    public Pelicula? GetById(int id);

    public Pelicula Create(PeliculaDTO p);

    public void Delete(int id);

    public Pelicula? Update(int id, Pelicula p);

    public IEnumerable<Critica> GetCriticas(int id);
}