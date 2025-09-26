namespace PokedexApi.Gateways;

using PokedexApi.Models;
//Como si fuera un repositorio

public interface IPokemonGateway
{
    Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IList<Pokemon>> GetPokemonsByNameAsync(string name, CancellationToken cancellationToken);

    Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);

    Task DeletePokemonAsync(Guid id, CancellationToken cancellationToken);

}