using Microsoft.EntityFrameworkCore;
using CancionesApi.Infrastructure;
using CancionesApi.Models;
using CancionesApi.Mappers;
using CancionesApi.Dtos;

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

    public async Task<Cancion> GetCancionByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cancion = await _context.Canciones.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return cancion.ToModel();
    }

    public async Task DeleteCancionAsync(Cancion cancion, CancellationToken cancellationToken)
    {
        _context.Canciones.Remove(cancion.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Cancion>> GetSongsByArtist(string artist, CancellationToken cancellationToken)
    {
        var songs = await _context.Canciones.AsNoTracking().Where(s => s.Artist.Contains(artist)).ToListAsync(cancellationToken);
        return songs.ToModel();
    }

    public async Task UpdateSongAsync(Cancion song, CancellationToken cancellationToken)
    {
        _context.Canciones.Update(song.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PagedResponseDto> GetSongsAsync(QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        IQueryable<Infrastructure.Entities.CancionEntity> query = _context.Canciones.AsNoTracking();
        if (!string.IsNullOrEmpty(queryParameters.Artist))
        {
            query = query.Where(p => p.Artist.ToLower() == queryParameters.Artist.ToLower());
        }

        if (!string.IsNullOrEmpty(queryParameters.Title))
        {
            query = query.Where(p => p.Title.ToLower().Contains(queryParameters.Title.ToLower()));
        }

        var orderByField = queryParameters.OrderBy.ToLower();
        var isAscending = queryParameters.OrderDirection.ToLower() == "asc";

        query = orderByField switch
        {
            "title" => isAscending ? query.OrderBy(p => p.Title) : query.OrderByDescending(p => p.Title),
            "artist" => isAscending ? query.OrderBy(p => p.Artist) : query.OrderByDescending(p => p.Artist),
            "album" => isAscending ? query.OrderBy(p => p.Album) : query.OrderByDescending(p => p.Album),
            "genre" => isAscending ? query.OrderBy(p => p.Genre) : query.OrderByDescending(p => p.Genre),
            "year" => isAscending ? query.OrderBy(p => p.Year) : query.OrderByDescending(p => p.Year),
            _ => isAscending ? query.OrderBy(p => p.Title) : query.OrderByDescending(p => p.Title)
        };

        var totalSongs = await query.CountAsync(cancellationToken);

        var pagination = await query.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).ToListAsync(cancellationToken);

        var songs = pagination.ToModel().ToResponseDto().ToList();

        return new PagedResponseDto
        {
            TotalRecords = totalSongs,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            TotalPages = (int)Math.Ceiling(totalSongs / (double)queryParameters.PageSize),
            Data = songs.ToList()
        };
    }
}