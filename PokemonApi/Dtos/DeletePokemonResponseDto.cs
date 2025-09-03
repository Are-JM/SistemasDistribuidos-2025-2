using System.Runtime.Serialization;

namespace PokemonApi.Dtos;

[DataContract(Name = "PokemonService", Namespace = "https://pokemons-api/pokemon-service")]

public class DeletePokemonResponseDto
{
    [DataMember(Name = "Success", Order = 1)]
    public bool Success { get; set; }
}