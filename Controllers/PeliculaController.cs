using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/peliculas")]
public class PeliculaController : ControllerBase {
    private readonly IPeliculaService _peliculaService;

    public PeliculaController(IPeliculaService peliculaService)
    {
        _peliculaService = peliculaService;
    }

    [HttpGet]
    public ActionResult<List<Pelicula>> GetAllPeliculas()
    {
        return Ok(_peliculaService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Pelicula> GetById(int id)
    {
        Pelicula? p = _peliculaService.GetById(id);
        if(p == null)
        {
            return NotFound("Pelicula no entrada");
        }
        return Ok(p);
    }

    [HttpGet("{id}/criticas")]
    public ActionResult<List<Critica>> GetCriticas(int id)
    {
        var p = _peliculaService.GetCriticas(id);
        return Ok(p);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult<Pelicula> NuevaPelicula(PeliculaDTO p)
    {
        Pelicula _p = _peliculaService.Create(p);
        return CreatedAtAction(nameof(GetById), new {id = _p.Id}, _p);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var p = _peliculaService.GetById(id);

        if(p == null)
        {
            return NotFound("Pelicula no encontrada!!!");
        }

        _peliculaService.Delete(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public ActionResult<Pelicula> UpdatePelicula(int id, Pelicula updatedPelicula)
    {
        if(id != updatedPelicula.Id)
        {
            return BadRequest("El ID de la pelicula en la URL no coincide con el ID de la pelicula en el cuerpo de la solicitud.");
        }
        var pelicula = _peliculaService.Update(id, updatedPelicula);

        if(pelicula is null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(GetById), new{id = pelicula.Id}, pelicula);
    }
}