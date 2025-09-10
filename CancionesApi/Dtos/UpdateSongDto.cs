using System.Runtime.Serialization;

namespace CancionesApi.Dtos;

[DataContract(Name = "UpdateSongDto", Namespace = "http://canciones-api/canciones-dto")]

public class UpdateSongDto
{
    [DataMember(Name = "Id", Order = 1)]
    public Guid Id { get; set; }

    [DataMember(Name = "Title", Order = 2)]
    public string Title { get; set; }

    [DataMember(Name = "Artist", Order = 3)]
    public string Artist { get; set; }

    [DataMember(Name = "Album", Order = 4)]
    public string Album { get; set; }

    [DataMember(Name = "Genre", Order = 5)]
    public string Genre { get; set; }

    [DataMember(Name = "Year", Order = 6)]
    public int Year { get; set; }

}