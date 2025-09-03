using PokemonApi.Models;

namespace PokemonApi.Repositories;

public interface IPokemonRepository
{
    Task<Pokemon> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Pokemon> CreateAsync(Pokemon pokemon, CancellationToken cancellationToken);
    Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Pokemon>> GetPokemonsByNameAsync(string name, CancellationToken cancellationToken);

    Task DeletePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);

    Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);
}