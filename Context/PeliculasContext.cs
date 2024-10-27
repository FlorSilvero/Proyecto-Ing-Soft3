using Microsoft.EntityFrameworkCore;

public class PeliculasContext:DbContext
{
  public DbSet<Pelicula> Peliculas {get; set;}
  public DbSet<Critica> Criticas {get; set;}

  public PeliculasContext(DbContextOptions<PeliculasContext> options) : base(options)
  {

  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Pelicula>(entity =>
    {
      entity.Property(p => p.Titulo).IsRequired().HasMaxLength(100);
      entity.Property(p => p.Descripcion).IsRequired().HasMaxLength(100);
      entity.Property(p => p.FechaLanzamiento).IsRequired(); //no se si aca va esto porque es una fecha en si o un numero
    }
    );

    modelBuilder.Entity<Critica>(entity => 
    {
      entity.Property(c => c.Descripcion).IsRequired();
      entity.Property(c => c.Puntaje).IsRequired();

      entity.HasOne(c => c.Pelicula).WithMany(p => p.Criticas).HasForeignKey(c => c.PeliculaId).IsRequired();

      entity.HasIndex(c => new {c.UsuarioId, c.PeliculaId}).IsUnique();
    }
    );
  }

} 