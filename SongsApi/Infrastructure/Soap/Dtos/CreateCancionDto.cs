using System.Runtime.Serialization;

namespace SongsApi.Infrastructure.Soap.Contracts;

[DataContract(Name = "CreateCancionDto", Namespace = "http://canciones-api/canciones-service")]

public class CreateCancionDto
{
    [DataMember(Name = "Title", Order = 1)]
    public string? Title { get; set; }

    [DataMember(Name = "Artist", Order = 2)]
    public string? Artist { get; set; }

    [DataMember(Name = "Album", Order = 3)]
    public string? Album { get; set; }

    [DataMember(Name = "Genre", Order = 4)]
    public string? Genre { get; set; }

    [DataMember(Name = "Year", Order = 5)]
    public int Year { get; set; }
    


}