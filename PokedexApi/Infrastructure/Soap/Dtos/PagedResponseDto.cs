using System.Runtime.Serialization;
using System.Collections.Generic;

namespace  PokedexApi.Infrastructure.Soap.Contracts;

[DataContract(Name = "PagedResponseDto", Namespace = "http://pokemon-api/pokemon-service")]
public class PagedResponseDto
{
    [DataMember(Order = 1)]
    public int PageNumber { get; set; }

    [DataMember(Order = 2)]
    public int PageSize { get; set; }

    [DataMember(Order = 3)]
    public int TotalRecords { get; set; }

    [DataMember(Order = 4)]
    public int TotalPages { get; set; }

    [DataMember(Order = 5)]
    public List<PokemonResponseDto> Data { get; set; } = new List<PokemonResponseDto>();
}