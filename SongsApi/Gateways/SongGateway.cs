using System.ServiceModel;
using SongsApi.Models;
using SongsApi.Infrastructure.Soap.Contracts;
using SongsApi.Mappers;

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
    
}