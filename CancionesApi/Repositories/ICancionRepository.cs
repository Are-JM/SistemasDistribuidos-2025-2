using CancionesApi.Models;
using CancionesApi.Dtos;

namespace CancionesApi.Repositories;

public interface ICancionRepository
{
    Task<Cancion> CreateCancionAsync(Cancion cancion, CancellationToken cancellationToken);

    Task<Cancion> GetByTitleAndArtistAsync(string title, string artist, CancellationToken cancellationToken);

    Task<Cancion> GetCancionByIdAsync(Guid id, CancellationToken cancellationToken);

    Task DeleteCancionAsync(Cancion cancion, CancellationToken cancellationToken);

    Task UpdateSongAsync(Cancion song, CancellationToken cancellationToken);

    Task<IReadOnlyList<Cancion>> GetSongsByArtist(string artist, CancellationToken cancellationToken);

    Task<PagedResponseDto> GetSongsAsync(QueryParameters queryParameters, CancellationToken cancellationToken);
}