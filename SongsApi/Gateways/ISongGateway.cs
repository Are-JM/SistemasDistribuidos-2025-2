namespace SongsApi.Gateways;

using SongsApi.Models;

public interface ISongGateway
{
    Task<Song> GetSongByIdAsync(Guid id, CancellationToken cancellationToken);
}