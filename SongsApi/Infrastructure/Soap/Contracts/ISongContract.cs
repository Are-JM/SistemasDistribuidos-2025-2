using System.ServiceModel;

namespace SongsApi.Infrastructure.Soap.Contracts;

[ServiceContract(Name = "CancionesService", Namespace = "http://canciones-api/canciones-service")]

public interface ISongContract
{
    [OperationContract]
    Task<CancionResponseDto> GetCancionById(Guid Id, CancellationToken cancellationToken);
}