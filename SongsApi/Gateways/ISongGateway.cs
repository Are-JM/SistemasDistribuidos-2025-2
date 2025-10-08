namespace SongsApi.Gateways;
using SongsApi.Models;

public interface ISongGateway
{
    Task<Song> GetSongByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IList<Song>> GetSongsByArtistAsync(string title, CancellationToken cancellationToken);

    Task<Song> CreateSongAsync(Song song, CancellationToken cancellationToken);

    Task DeleteSongAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateSongAsync(Song song, CancellationToken cancellationToken);

    Task<PagedResult<Song>> GetSongsAsync(string title, string artist, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken);
}