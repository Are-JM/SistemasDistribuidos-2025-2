namespace SongsApi.Services;

using SongsApi.Models;
public interface ISongService
{
    Task<Song> GetSongByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Song> CreateSongAsync(Song song, CancellationToken cancellationToken);

    Task<PagedResult<Song>> GetSongsAsync(string title, string artist, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken);

    Task DeleteSongAsync(Guid id, CancellationToken cancellationToken);

    Task UpdateSongAsync(Song song, CancellationToken cancellationToken);
    
    Task<Song> PatchSongAsync(Guid id, string? title, string? artist, string? album, string? genre, int? year, CancellationToken cancellationToken);
}