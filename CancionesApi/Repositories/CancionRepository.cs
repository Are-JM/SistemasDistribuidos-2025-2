using Microsoft.EntityFrameworkCore;
using CancionesApi.Infrastructure;
using CancionesApi.Models;
using CancionesApi.Mappers;

namespace CancionesApi.Repositories;

public class CancionRepository : ICancionRepository
{
    private readonly RelationalDbContext _context;

    public CancionRepository(RelationalDbContext context)
    {
        _context = context;
    }

    public async Task<Cancion> CreateCancionAsync(Cancion cancion, CancellationToken cancellationToken)
    {
        var cancionToCreate = cancion.ToEntity();
        cancionToCreate.Id = Guid.NewGuid();
        await _context.Canciones.AddAsync(cancionToCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cancionToCreate.ToModel();
    }

    public async Task<Cancion> GetByTitleAndArtistAsync(string title, string artist, CancellationToken cancellationToken)
    {
        var cancion = await _context.Canciones.AsNoTracking()
        .FirstOrDefaultAsync(s => s.Title.Contains(title) && s.Artist.Contains(artist), cancellationToken);
        return cancion.ToModel();
    }

}