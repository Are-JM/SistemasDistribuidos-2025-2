using System.ServiceModel.Channels;
using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;
using PokedexApi.Services;
using PokedexApi.Mappers;
using PokemonApi.Dtos;
using PokedexApi.Exceptions;

namespace PokedexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")] //api/v1/Pokemons
public class PokemonsController : ControllerBase
{
    private readonly IPokemonService _pokemonService;
    public PokemonsController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }
    
    //localhost:PORT/api/v1/pokemons/ID
    [HttpGet("{id}", Name = "GetPokemonByIdAsync")]

    public async Task<ActionResult<PokemonResponse>> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id, cancellationToken);
        return pokemon is null ? NotFound() : Ok(pokemon.ToResponse());
    }

    //localhost:PORT/api/v1/pokemons?name=pikachu&type=electric
    //HTTP STATUS GET
    //200 OK
    //400 BAD REQUEST
    //508 Internal Server Error
    [HttpGet]
    public async Task<ActionResult<IList<PokemonResponse>>> GetPokemonsAsync([FromQuery] string name, [FromQuery] string type,
    [FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string orderBy, [FromQuery] string orderDirection, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(type))
        {
            return BadRequest(new { Message = "Type query parameter is required" });
        }
        var result = await _pokemonService.GetPokemonsAsync(name, type,pageSize,pageNumber,orderBy,orderDirection, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PokemonResponse>> CreatePokemonAsync([FromBody] CreatePokemonRequest createPokemon, CancellationToken cancellationToken)
    {
        try
        {
            if (!IsValidAttack(createPokemon))
            {
                return BadRequest(new { Message = "Attack doesn't have a valid value" });
            }

            var pokemon = await _pokemonService.CreatePokemonAsync(createPokemon.ToModel(), cancellationToken);
            return CreatedAtRoute(nameof(GetPokemonByIdAsync), new { id = pokemon.Id }, pokemon.ToResponse());
        }
        catch (PokemonAlreadyExistsException e)
        {

            return Conflict(new { Message = e.Message });
        }
       
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePokemonAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _pokemonService.DeletePokemonAsync(id, cancellationToken);
            return NoContent();

        }
        catch (PokemonNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    private static bool IsValidAttack(CreatePokemonRequest createPokemon)
    {
        return createPokemon.Stats.Attack > 0;
    }
}