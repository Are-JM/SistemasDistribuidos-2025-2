using SongsApi.Gateways;
using SongsApi.Models;
using SongsApi.Exceptions;

namespace SongsApi.Services;

public class SongService : ISongService
{
    private readonly ISongGateway _songGateway;

    public SongService(ISongGateway songGateway)
    {
        _songGateway = songGateway;
    }
    public async Task<Song> GetSongByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _songGateway.GetSongByIdAsync(id, cancellationToken);
    }

    public async Task DeleteSongAsync(Guid id, CancellationToken cancellationToken)
    {
        await _songGateway.DeleteSongAsync(id, cancellationToken);
    }

    public async Task UpdateSongAsync(Song song, CancellationToken cancellationToken)
    {
        await _songGateway.UpdateSongAsync(song, cancellationToken);
    }

    public async Task<Song> PatchSongAsync(Guid id, string? title, string? artist, string? album, string? genre, int? year, CancellationToken cancellationToken)
    {
        var song = await _songGateway.GetSongByIdAsync(id, cancellationToken);
        if (song is null)
        {
            throw new SongNotFoundException(id);
        }

        song.Title = title ?? song.Title;
        song.Artist = artist ?? song.Artist;
        song.Album = album ?? song.Album;
        song.Genre = genre ?? song.Genre;
        song.Year = year ?? song.Year;

        await _songGateway.UpdateSongAsync(song, cancellationToken);
        return song;
    }

    public async Task<PagedResult<Song>> GetSongsAsync(string title, string artist, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken)
    {
        return await _songGateway.GetSongsAsync(title, artist, pageSize, pageNumber, orderBy, orderDirection, cancellationToken);
    }
    public async Task<Song> CreateSongAsync(Song song, CancellationToken cancellationToken)
    {
        var songs = await _songGateway.GetSongsByArtistAsync(song.Title, cancellationToken);
        if (SongExists(songs, song.Title, song.Artist))
        {
            throw new SongAlreadyExistsException(song.Title);
        }
        return await _songGateway.CreateSongAsync(song, cancellationToken);
    }
    
    
    public static bool SongExists(IList<Song> songs, string songTitle, string songArtist)
    {
        return songs.Any(s => s.Title.ToLower().Equals(songTitle.ToLower()) && s.Artist.ToLower().Equals(songArtist.ToLower()));
    }
}