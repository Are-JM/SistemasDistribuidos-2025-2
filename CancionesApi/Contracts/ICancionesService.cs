using System.ServiceModel;
using CancionesApi.Dtos;
using CancionesApi.Models;

namespace CancionesApi.Services;

[ServiceContract(Name = "CancionesService", Namespace = "http://canciones-api/canciones-service")]

public interface ICancionesService
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
}