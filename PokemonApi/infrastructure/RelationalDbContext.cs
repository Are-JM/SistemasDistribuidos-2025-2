using Microsoft.EntityFrameworkCore;
using PokemoApi.Infrastructure.Entities;

namespace PokemonApi.Infrastructure;

public class RelationalDbContext : DbContext
{
    public DbSet<PokemonEntity> Pokemons { get; set; }
    public RelationalDbContext(DbContextOptions<RelationalDbContext> db) : base(db)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)//Nos va a permitir modificar nuestra tabla, su tamaño, especificaciones, etc
    //Va a sobreescribir lo que se hizo en la clase padre, solo pueden acceder los hijos
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PokemonEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Level).IsRequired();
            entity.Property(e => e.Attack).IsRequired();
            entity.Property(e => e.Defense).IsRequired();
            entity.Property(e => e.Speed).IsRequired();

        });
    }
}