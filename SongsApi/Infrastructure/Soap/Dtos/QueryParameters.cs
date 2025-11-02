using System.Runtime.Serialization;

namespace SongsApi.Infrastructure.Soap.Contracts;

[DataContract(Name = "QueryParameters", Namespace = "http://canciones-api/canciones-service")]
public class QueryParameters
{
    [DataMember(Order = 1)]
    public string Title { get; set; }

    [DataMember(Order = 2)]
    public string Artist { get; set; }

    [DataMember(Order = 3)]
    public int PageSize { get; set; }

    [DataMember(Order = 4)]
    public int PageNumber { get; set; }

    [DataMember(Order = 5)]
    public string OrderBy { get; set; } = string.Empty;

    [DataMember(Order = 6)]
    public string OrderDirection { get; set; } = string.Empty;
}