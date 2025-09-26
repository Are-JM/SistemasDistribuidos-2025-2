using System.ServiceModel;
using PokedexApi.Dtos;
using PokedexApi.Models;

namespace PokedexApi.Infrastructure.Soap.Contracts;
[ServiceContract (Name = "PokemonService", Namespace = "http://pokemon-api/pokemon-service")]
public interface IPokemonContract
{
    [OperationContract]
    Task<PokemonResponseDto> UpdatePokemon(UpdatePokemonDto pokemon, CancellationToken cancellationToken);

    [OperationContract]
    Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemon, CancellationToken cancellationToken);

    [OperationContract]
    Task<PokemonResponseDto> GetPokemonById(Guid Id, CancellationToken cancellationToken);

    [OperationContract]
    Task<IList<PokemonResponseDto>> GetPokemonsByName(string Name, CancellationToken cancellationToken);

    [OperationContract]
    Task<DeletePokemonResponseDto> DeletePokemonAsync(Guid id, CancellationToken cancellationToken);
}