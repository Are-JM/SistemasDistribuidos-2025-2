using System.ServiceModel.Channels;
using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;
using PokedexApi.Services;
using PokedexApi.Mappers;
using PokedexApi.Exceptions;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize(Policy = "Read")]
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
    [Authorize(Policy = "Read")]

    public async Task<ActionResult<PaginatedResponse>> GetPokemonsAsync([FromQuery] string name, [FromQuery] string type,
    [FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string orderBy, [FromQuery] string orderDirection, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(type))
        {
            return BadRequest(new { Message = "Type query parameter is required" });
        }
        var result = await _pokemonService.GetPokemonsAsync(name, type,pageSize,pageNumber,orderBy,orderDirection, cancellationToken);
        return Ok(result.ToPagedResponse());
    }

    [HttpPost]
    [Authorize(Policy = "Write")]

    public async Task<ActionResult<PokemonResponse>> CreatePokemonAsync([FromBody] CreatePokemonRequest createPokemon, CancellationToken cancellationToken)
    {
        try
        {
            if (!IsValidAttack(createPokemon.Stats.Attack))
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
    [Authorize(Policy = "Write")]
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

    [HttpPut("{id}")]
    [Authorize(Policy = "Write")]
    public async Task<ActionResult> UpdatePokemonAsync(Guid id, [FromBody] UpdatePokemonRequest pokemon, CancellationToken cancellationToken)
    {

        try {
            if (!IsValidAttack(pokemon.Stats.Attack)) {
                return BadRequest(new { Message = "Invalid attack value" });
            }
            await _pokemonService.UpdatePokemonAsync(pokemon.ToModel(id), cancellationToken);
            return NoContent();
        }
        catch (PokemonNotFoundException)
        {
            return NotFound();
        } catch (PokemonAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }

    [HttpPatch("{id}")]
    [Authorize(Policy = "Write")]
    public async Task<ActionResult<PokemonResponse>> PatchPokemonAsync(Guid id, [FromBody] PatchPokemonRequest pokemonRequest, CancellationToken cancellationToken)
    {
        try
        {
            if (pokemonRequest.Attack.HasValue && !IsValidAttack(pokemonRequest.Attack.Value)) {
                return BadRequest(new{Message = "Invalid attack value"}); //400
            }

            var pokemon = await _pokemonService.PatchPokemonAsync(id, pokemonRequest.Name, pokemonRequest.Type, pokemonRequest.Attack, pokemonRequest.Defense, pokemonRequest.Speed, cancellationToken);
            return Ok(pokemon.ToResponse()); // 204
        }
        catch (PokemonNotFoundException)
        {
            return NotFound(); // 404
        }
        catch (PokemonAlreadyExistsException ex)
        {
            return Conflict(new {Message = ex.Message}); // 409
        }
    }

    private static bool IsValidAttack(int attack)
    {
        return attack > 0;
    }
}