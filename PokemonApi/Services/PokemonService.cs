using System.ServiceModel;
using PokemonApi.Dtos;
using PokemonApi.Repositories;
using PokemonApi.Mappers;
using PokemonApi.Validators;
using PokemonApi.Models;

namespace PokemonApi.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;
    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PokemonResponseDto> UpdatePokemon(UpdatePokemonDto pokemonToUpdate, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonByIdAsync(pokemonToUpdate.Id, cancellationToken);
        if (!PokemonExist(pokemon))
        {
            throw new FaultException(reason: "Pokemon not found");
        }

        if(!await IsPokemonAllowedToBeUpdated(pokemonToUpdate, cancellationToken))
        {
            throw new FaultException("Another pokemon with the same name already exists");
        }



        pokemon.Name = pokemonToUpdate.Name;
        pokemon.Type = pokemonToUpdate.Type;
        pokemon.Stats.Attack = pokemonToUpdate.Stats.Attack;
        pokemon.Stats.Defense = pokemonToUpdate.Stats.Defense;
        pokemon.Stats.Speed = pokemonToUpdate.Stats.Speed;

        await _pokemonRepository.UpdatePokemonAsync(pokemon, cancellationToken);
        return pokemon.ToResponseDto();
    }


    private async Task<bool> IsPokemonAllowedToBeUpdated(UpdatePokemonDto pokemonToUpdate, CancellationToken cancellationToken)
    {
        var duplicatedPokemon = await _pokemonRepository.GetByNameAsync(pokemonToUpdate.Name, cancellationToken);
        return duplicatedPokemon is null || IsTheSamePokemon(duplicatedPokemon, pokemonToUpdate);
    }   

    private bool IsTheSamePokemon(Pokemon pokemon, UpdatePokemonDto pokemonToUpdate)
    {
        return pokemon.Id != pokemonToUpdate.Id;
    }
    public async Task<DeletePokemonResponseDto> DeletePokemonAsync(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);
        if (!PokemonExist(pokemon))
        {
            throw new FaultException("Pokemon not found");
        }
        await _pokemonRepository.DeletePokemonAsync(pokemon, cancellationToken);
        return new DeletePokemonResponseDto { Success = true };
    }


    public async Task<IList<PokemonResponseDto>> GetPokemonsByName(string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonsByNameAsync(name, cancellationToken);
        return pokemon.ToResponseDto();

    }

    public async Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);
        return PokemonExist(pokemon) ? pokemon.ToResponseDto() : throw new FaultException("Pokemon doesn't exist");
    }

    public async Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemonRequest, CancellationToken cancellationToken)
    {
        pokemonRequest.ValidateName().ValidateLevel().ValidateType();
        if (await PokemonAlreadyExists(pokemonRequest.Name, cancellationToken))
        {
            throw new FaultException("Pokemon already exists");
        }

        var pokemon = await _pokemonRepository.CreateAsync(pokemonRequest.ToModel(), cancellationToken);
        return pokemon.ToResponseDto();
    }

    private static bool PokemonExist(Pokemon? pokemon)
    {
        return pokemon is not null;
    }

    private async Task<bool> PokemonAlreadyExists(string name, CancellationToken cancellationToken)
    {
        var pokemons = await _pokemonRepository.GetByNameAsync(name, cancellationToken);
        return pokemons is not null;
    }
}