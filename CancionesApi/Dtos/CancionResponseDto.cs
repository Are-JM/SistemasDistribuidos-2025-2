using System.Runtime.Serialization;

namespace CancionesApi.Dtos;

[DataContract(Name = "CancionResponseDto", Namespace = "http://canciones-api/canciones-service")]

public class CancionResponseDto
{
    [DataMember(Name = "Id", Order = 1)]
    public Guid Id { get; set; }

    [DataMember(Name = "Title", Order = 2)]
    public required string Title { get; set; }

    [DataMember(Name = "Artist", Order = 3)]
    public required string Artist { get; set; }

    [DataMember(Name = "Album", Order = 4)]
    public required string Album { get; set; }

    [DataMember(Name = "Genre", Order = 5)]
    public required string Genre { get; set; }

    [DataMember(Name = "Year", Order = 6)]
    public int Year { get; set; }
}