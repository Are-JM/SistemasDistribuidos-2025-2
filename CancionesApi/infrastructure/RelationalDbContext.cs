using Microsoft.EntityFrameworkCore;
using CancionesApi.Infrastructure.Entities;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CancionesApi.Infrastructure;

public class RelationalDbContext : DbContext
{
    public DbSet<CancionEntity> Canciones { get; set; }

    public RelationalDbContext(DbContextOptions<RelationalDbContext> db) : base(db)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CancionEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Artist).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Album).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Genre).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Year).IsRequired();
        });
    }
}