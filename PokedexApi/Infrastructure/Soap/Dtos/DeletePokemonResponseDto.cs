using System.Runtime.Serialization;

namespace PokedexApi.Infrastructure.Soap.Contracts;

[DataContract(Name = "DeletePokemonResponseDto", Namespace = "http://pokemon-api/pokemon-dto")]

public class DeletePokemonResponseDto
{
    [DataMember(Name = "Success", Order = 1)]
    public bool Success { get; set; }
}