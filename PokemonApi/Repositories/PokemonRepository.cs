using Microsoft.EntityFrameworkCore;
using PokemonApi.Infrastructure;
using PokemonApi.Models;
using PokemonApi.Mappers;

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
}