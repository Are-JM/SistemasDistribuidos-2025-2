using Microsoft.EntityFrameworkCore;
using PokemonApi.Infrastructure;
using PokemonApi.Models;
using PokemonApi.Mappers;
using PokemonApi.Dtos;

namespace PokemonApi.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly RelationalDbContext _context;

    public PokemonRepository(RelationalDbContext context)
    {
        _context = context;

    }

    public async Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        _context.Pokemons.Update(pokemon.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        _context.Pokemons.Remove(pokemon.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IReadOnlyList<Pokemon>> GetPokemonsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var pokemons = await _context.Pokemons.AsNoTracking()
        .Where(s => s.Name.Contains(name))
        .ToListAsync(cancellationToken);

        return pokemons.ToModel();
    }

    public async Task<Pokemon> GetPokemonByIdAsync(Guid Id, CancellationToken cancellationtoken)
    {
        var pokemon = await _context.Pokemons.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id, cancellationtoken);
        return pokemon.ToModel();
    }
    public async Task<Pokemon> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var pokemon = await _context.Pokemons.AsNoTracking().FirstOrDefaultAsync(s => s.Name.Contains(name));
        return pokemon.ToModel();
    }

    public async Task<Pokemon> CreateAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        var pokemonToCreate = pokemon.ToEntity();
        pokemonToCreate.Id = Guid.NewGuid();
        await _context.Pokemons.AddAsync(pokemonToCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return pokemonToCreate.ToModel();
    }

    public async Task<PagedResponseDto> GetPokemonsAsync(QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        IQueryable<Infrastructure.Entities.PokemonEntity> query = _context.Pokemons.AsNoTracking();
        if (!string.IsNullOrEmpty(queryParameters.Type))
        {
            query = query.Where(p => p.Type.ToLower() == queryParameters.Type.ToLower());
        }

        if (!string.IsNullOrEmpty(queryParameters.Name))
        {
            query = query.Where(p => p.Name.ToLower().Contains(queryParameters.Name.ToLower()));
        }

        var orderByField = queryParameters.OrderBy.ToLower();
        var isAscending = queryParameters.OrderDirection.ToLower() == "asc";

        query = orderByField switch
        {
            "name" => isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
            "level" => isAscending ? query.OrderBy(p => p.Level) : query.OrderByDescending(p => p.Level),
            "attack" => isAscending ? query.OrderBy(p => p.Attack) : query.OrderByDescending(p => p.Attack),
            "defense" => isAscending ? query.OrderBy(p => p.Defense) : query.OrderByDescending(p => p.Defense),
            _ => isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name)
        };

        var totalPokemons = await query.CountAsync(cancellationToken);

        var pagination = await query.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).ToListAsync(cancellationToken);

        var pokemons = pagination.ToModel().ToResponseDto().ToList();

        return new PagedResponseDto
        {
            TotalRecords = totalPokemons,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            TotalPages = (int)Math.Ceiling(totalPokemons / (double)queryParameters.PageSize),
            Data = pokemons.ToList()
        };
    }
}