using SongsApi.Gateways;
using SongsApi.Models;

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

}