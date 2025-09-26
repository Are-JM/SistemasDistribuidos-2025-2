using PokedexApi.Gateways;
using PokedexApi.Models;
using PokedexApi.Exceptions;
using PokedexApi.Dtos;
using System.Linq.Dynamic.Core;

namespace PokedexApi.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonGateway _pokemonGateway;

    public PokemonService(IPokemonGateway pokemonGateway)
    {
        _pokemonGateway = pokemonGateway;
    }
    public async Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _pokemonGateway.GetPokemonByIdAsync(id, cancellationToken);
    }

    public async Task<PaginatedResponse<PokemonResponse>> GetPokemonsAsync(string name, string type, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken)
    {
        var pokemons = await _pokemonGateway.GetPokemonsByNameAsync(name, cancellationToken);
        var filteredpokemons = pokemons.Where(s => s.Type.ToLower().Contains(type.ToLower())).AsQueryable();
        filteredpokemons = filteredpokemons.OrderBy($"{orderBy} {orderDirection}");

        var pagedList = filteredpokemons.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        var totalRecords = filteredpokemons.Count();
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        return new PaginatedResponse<PokemonResponse>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            Data = pagedList.Select(p => new PokemonResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Type
                }).ToList()
        };
    }
    public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        var pokemons = await _pokemonGateway.GetPokemonsByNameAsync(pokemon.Name, cancellationToken);
        if (PokemonExists(pokemons, pokemon.Name))
        {
            throw new PokemonAlreadyExistsException(pokemon.Name);
        }
        return await _pokemonGateway.CreatePokemonAsync(pokemon, cancellationToken);
    }

    public async Task DeletePokemonAsync(Guid id, CancellationToken cancellationToken)
    {
        await _pokemonGateway.DeletePokemonAsync(id, cancellationToken);
    }
    
    public static bool PokemonExists(IList<Pokemon> pokemons, string pokemonNameToSearch)
    {
        return pokemons.Any(p => p.Name.ToLower().Equals(pokemonNameToSearch.ToLower()));
    }

}