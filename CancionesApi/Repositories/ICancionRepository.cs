using CancionesApi.Models;

namespace CancionesApi.Repositories;

public interface ICancionRepository
{
    Task<Cancion> CreateCancionAsync(Cancion cancion, CancellationToken cancellationToken);

    Task<Cancion> GetByTitleAndArtistAsync(string title, string artist, CancellationToken cancellationToken);

    Task<Cancion> GetCancionByIdAsync(Guid id, CancellationToken cancellationToken);

    Task DeleteCancionAsync(Cancion cancion,CancellationToken cancellationToken);
}