public interface ICriticaService
{
    public IEnumerable<Critica> GetAll();

    public Critica? GetById(int id);

    public Critica Create(CriticaDTO c);

    public bool Delete(int id, string UsuarioId);

    public Critica Update(int id, CriticaDTO c, string UsuarioId);

    public IEnumerable<Critica> GetByUser(string UsuarioId);
}