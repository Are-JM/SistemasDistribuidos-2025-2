namespace SongsApi.Services;

using SongsApi.Models;
public interface ISongService
{
    Task<Song> GetSongByIdAsync(Guid id, CancellationToken cancellationToken);

}