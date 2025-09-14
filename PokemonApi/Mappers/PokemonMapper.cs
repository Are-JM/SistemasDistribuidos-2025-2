using Microsoft.AspNetCore.StaticAssets;
using PokemonApi.Dtos;
using PokemonApi.Infrastructure.Entities;
using PokemonApi.Models;

namespace PokemonApi.Mappers;

public static class PokemonMapper
{
    //extension method
    public static Pokemon ToModel(this PokemonEntity pokemonEntity)
    {
        if (pokemonEntity is null)
        {
            return null;
        }

        return new Pokemon
        {
            Id = pokemonEntity.Id,
            Name = pokemonEntity.Name,
            Type = pokemonEntity.Type,
            Level = pokemonEntity.Level,
            Stats = new Stats
            {
                Attack = pokemonEntity.Attack,
                Speed = pokemonEntity.Speed,
                Defense = pokemonEntity.Defense,
                Stamina = pokemonEntity.Stamina
            }
        };
    }

    public static PokemonEntity ToEntity(this Pokemon pokemon)
    {
        if (pokemon is null)
        {
            return null;
        }

        return new PokemonEntity
        {
            Id = pokemon.Id,
            Level = pokemon.Level,
            Type = pokemon.Type,
            Name = pokemon.Name,
            Attack = pokemon.Stats.Attack,
            Speed = pokemon.Stats.Speed,
            Defense = pokemon.Stats.Defense,
            Stamina = pokemon.Stats.Stamina
        };
    }

    public static Pokemon ToModel(this CreatePokemonDto requestPokemonDto)
    {
        return new Pokemon
        {
            Name = requestPokemonDto.Name,
            Type = requestPokemonDto.Type,
            Level = requestPokemonDto.Level,
            Stats = new Stats
            {
                Attack = requestPokemonDto.Stats.Attack,
                Speed = requestPokemonDto.Stats.Speed,
                Defense = requestPokemonDto.Stats.Defense,
                Stamina = requestPokemonDto.Stats.Stamina
            }
        };
    }
    public static PokemonResponseDto ToResponseDto(this Pokemon pokemon)
    {
        return new PokemonResponseDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Stats = new StatsDto
            {
                Attack = pokemon.Stats.Attack,
                Speed = pokemon.Stats.Speed,
                Defense = pokemon.Stats.Defense,
                Stamina = pokemon.Stats.Stamina
            }
        };
    }

    public static IList<PokemonResponseDto> ToResponseDto(this IReadOnlyList<Pokemon> pokemons)
    {
        return pokemons.Select(p => p.ToResponseDto()).ToList();
    }
    
    public static IReadOnlyList<Pokemon> ToModel(this IReadOnlyList<PokemonEntity> pokemons)
    {
        return pokemons.Select(s => s.ToModel()).ToList();
    }
}