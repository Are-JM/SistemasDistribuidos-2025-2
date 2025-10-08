using System.ServiceModel;

namespace SongsApi.Infrastructure.Soap.Contracts;

[ServiceContract(Name = "CancionesService", Namespace = "http://canciones-api/canciones-service")]

public interface ISongContract
{
    [OperationContract]
    Task<CancionResponseDto> CreateCancion(CreateCancionDto cancion, CancellationToken cancellationToken);

    [OperationContract]
    Task<CancionResponseDto> GetCancionById(Guid Id, CancellationToken cancellationToken);

    [OperationContract]
    Task<DeleteCancionResponseDto> DeleteCancionAsync(Guid Id, CancellationToken cancellationToken);

    [OperationContract]
    Task<CancionResponseDto> UpdateSong(UpdateSongDto song, CancellationToken cancellationToken);

    [OperationContract]
    Task<IList<CancionResponseDto>> GetSongsByArtist(string Artist, CancellationToken cancellationToken);

    [OperationContract]
    Task<PagedResponseDto> GetSongs(QueryParameters queryParameters, CancellationToken cancellationToken);

}