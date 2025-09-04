using System.ServiceModel;
using CancionesApi.Dtos;

namespace CancionesApi.Services;

[ServiceContract(Name = "CancionesService", Namespace = "http://canciones-api/canciones-service")]

public interface ICancionesService
{
    [OperationContract]

    Task<CancionResponseDto> CreateCancion(CreateCancionDto cancion, CancellationToken cancellationToken);
}