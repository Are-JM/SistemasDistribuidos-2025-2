using System.Runtime.Serialization;

namespace CancionesApi.Dtos;

[DataContract(Name = "DeleteCancionResponseDto", Namespace = "http://canciones-api/canciones-service")]

public class DeleteCancionResponseDto
{
    [DataMember(Name = "Success", Order = 1)]
    public bool Success { get; set; }
}