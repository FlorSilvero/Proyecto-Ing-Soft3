
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class CriticaDbService : ICriticaService
{
    private readonly PeliculasContext _context;

    public CriticaDbService(PeliculasContext context)
    {
        _context = context;
    }

    public Critica Create(CriticaDTO c)
    {
        var nuevaCritica = new Critica
        {
            Descripcion = c.Descripcion,
            Puntaje = c.Puntaje,
            PeliculaId = c.PeliculaId,
        };
        _context.Criticas.Add(nuevaCritica);
        _context.SaveChanges();
        return nuevaCritica;
    }

    /*public bool Delete(int id)
    {
        Critica? c = _context.Criticas.Find(id);
        if(c is null) return false;

        _context.Criticas.Remove(c);
        _context.SaveChanges();
        return true;
    }*/
    public bool Delete(int id, string userId)
    {
        // Buscar la crítica por su id
        var critica = _context.Criticas.FirstOrDefault(c => c.Id == id);

        // Verificar si la crítica existe
        if (critica == null)
        {
            throw new Exception("Crítica no encontrada.");
        }

        // Verificar si la crítica pertenece al usuario autenticado
        if (critica.UsuarioId != userId)
        {
            return false; // El usuario no tiene permiso para eliminar la crítica
        }

        // Eliminar la crítica
        _context.Criticas.Remove(critica);
        _context.SaveChanges();

        return true; // Eliminación exitosa
    }


    public IEnumerable<Critica> GetAll()
    {
        return _context.Criticas.Include(la => la.Pelicula);
    }

    public Critica? GetById(int id)
    {
        return _context.Criticas.Find(id);
    }

    public Critica Update(int id, CriticaDTO c, string UsuarioId)
    {
        // Buscar la crítica por ID
        var criticaUpdate = _context.Criticas.FirstOrDefault(c => c.Id == id);
        
        if (criticaUpdate == null)
        {
            throw new Exception("La crítica no existe.");
        }

        // Verificar si el usuario actual es el dueño de la crítica
        if (criticaUpdate.UsuarioId != UsuarioId)
        {
            throw new UnauthorizedAccessException("No tienes permiso para modificar esta crítica.");
        }

        // Actualizar los campos de la crítica
        criticaUpdate.Descripcion = c.Descripcion;
        criticaUpdate.Puntaje = c.Puntaje;
        criticaUpdate.PeliculaId = c.PeliculaId;

        // Guardar los cambios
        _context.Entry(criticaUpdate).State = EntityState.Modified;
        _context.SaveChanges();

        return criticaUpdate;
    }

    // Obtener críticas por userId proporcionado
    public IEnumerable<Critica> GetByUser(string UsuarioId)
    {
        return _context.Criticas.Where(c => c.UsuarioId == UsuarioId).ToList();
    }
}
