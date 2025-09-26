using PokedexApi.Models;
using PokedexApi.Infrastructure.Soap.Contracts;
using PokedexApi.Dtos;
using PokemonApi.Dtos;
namespace PokedexApi.Mappers;

public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonResponseDto pokemonResponseDto)
    {
        return new Pokemon
        {
            Id = pokemonResponseDto.Id,
            Name = pokemonResponseDto.Name,
            Type = pokemonResponseDto.Type,
            Level = pokemonResponseDto.Level,
            stats = new Stats
            {
                Attack = pokemonResponseDto.Stats.Attack,
                Defense = pokemonResponseDto.Stats.Defense,
                Speed = pokemonResponseDto.Stats.Speed
            }
        };
    }

    public static IList<Pokemon> ToModel(this IList<PokemonResponseDto> pokemonResponseDtos)
    {
        return pokemonResponseDtos.Select(s => s.ToModel()).ToList();

    }

    public static IList<PokemonResponse> ToResponse(this IList<Pokemon> pokemons)
    {
        return pokemons.Select(s => s.ToResponse()).ToList();
    }
    public static PokemonResponse ToResponse(this Pokemon pokemon)
    {
        return new PokemonResponse
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Attack = pokemon.stats.Attack
        };
    }

    public static Pokemon ToModel(this CreatePokemonRequest createPokemonRequest)
    {
        return new Pokemon
        {
            Name = createPokemonRequest.Name,
            Type = createPokemonRequest.Type,
            Level = createPokemonRequest.Level,
            stats = new Stats
            {
                Attack = createPokemonRequest.Stats.Attack,
                Defense = createPokemonRequest.Stats.Defense,
                Speed = createPokemonRequest.Stats.Speed
            }
        };
    }

    public static CreatePokemonDto ToRequest(this Pokemon pokemon) {
        return new CreatePokemonDto
        {
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Stats = new StatsDto
            {
                Attack = pokemon.stats.Attack,
                Defense = pokemon.stats.Defense,
                Speed = pokemon.stats.Speed
            }

        };
    }
}