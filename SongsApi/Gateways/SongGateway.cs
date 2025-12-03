using System.ServiceModel;
using SongsApi.Models;
using SongsApi.Infrastructure.Soap.Contracts;
using SongsApi.Mappers;
using SongsApi.Exceptions;

namespace SongsApi.Gateways;

public class SongGateway : ISongGateway
{
    private readonly ISongContract _songContract;
    private readonly ILogger<SongGateway> _logger;
    public SongGateway(IConfiguration configuration, ILogger<SongGateway> logger)
    {
        var binding = new BasicHttpBinding();
        var endpoint = new EndpointAddress(configuration.GetValue<string>("SongService:Url"));
        _songContract = new ChannelFactory<ISongContract>(binding, endpoint).CreateChannel();
        _logger = logger;
    }

    public async Task<Song> GetSongByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var song = await _songContract.GetCancionById(id, cancellationToken);
            return song.ToModel();
        }
        catch (FaultException ex) when (ex.Message == "Song doesn't exist")
        {
            return null;
        }
    }

    public async Task UpdateSongAsync(Song song, CancellationToken cancellationToken)
    {
        try
        {
            await _songContract.UpdateSong(song.ToUpdateRequest(), cancellationToken);
        }
        catch (FaultException ex) when (ex.Message == "Song not found")
        {
            throw new SongNotFoundException(song.Id);
        }
        catch (FaultException ex) when (ex.Message == "Another song by the same title and artist already exists")
        {
            throw new SongAlreadyExistsException(song.Title);
        }
    }

    public async Task DeleteSongAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _songContract.DeleteCancionAsync(id, cancellationToken);
        }
        catch (FaultException ex) when (ex.Message == "Song not found")
        {
            _logger.LogWarning(ex, "Song not found");
            throw new SongNotFoundException(id);
        }
    }

    public async Task<IList<Song>> GetSongsByArtistAsync(string artist, CancellationToken cancellationToken)
    {
        var songs = await _songContract.GetSongsByArtist(artist, cancellationToken);
        return songs.ToModel();
    }

    public async Task<Song> CreateSongAsync(Song song, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Sending request to SOAP API, with title: {Title}", song.Title);
            var createdSong = await _songContract.CreateCancion(song.ToRequest(), cancellationToken);
            return createdSong.ToModel();
        }

        catch (Exception ex) when (ex.Message.Contains("Song Already Exists"))
        {
            _logger.LogError(ex, "Something wrong in create song soap api");
            throw new Exception("Song Already Exists");
        }
    }

    public async Task<PagedResult<Song>> GetSongsAsync(string title, string artist, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken)
    {

        var queryParameters = new Infrastructure.Soap.Contracts.QueryParameters
        {
            Title = title,
            Artist = artist,
            PageSize = pageSize,
            PageNumber = pageNumber,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        };

        var result = await _songContract.GetSongs(queryParameters, cancellationToken);

        return result.ToPagedResult();

    }
}