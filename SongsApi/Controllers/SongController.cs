using System.ServiceModel.Channels;
using Microsoft.AspNetCore.Mvc;
using SongsApi.Dtos;
using SongsApi.Services;
using SongsApi.Mappers;
using SongsApi.Exceptions;

namespace SongsApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SongsController : ControllerBase
{
    private readonly ISongService _songService;
    public SongsController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet("{id}", Name = "GetSongByIdAsync")]
    public async Task<ActionResult<SongResponse>> GetSongByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var song = await _songService.GetSongByIdAsync(id, cancellationToken);
        return song is null ? NotFound() : Ok(song.ToResponse());
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse>> GetSongsAsync([FromQuery] string title, [FromQuery] string artist,
    [FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string orderBy, [FromQuery] string orderDirection, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(artist))
        {
            return BadRequest(new { Message = "Artist query parameter is required" });
        }
        var result = await _songService.GetSongsAsync(title, artist, pageSize, pageNumber, orderBy, orderDirection, cancellationToken);
        return Ok(result.ToPagedResponse());
    }

    [HttpPost]
    public async Task<ActionResult<SongResponse>> CreateSongAsync([FromBody] CreateSongRequest createSong, CancellationToken cancellationToken)
    {
        try
        {
            var song = await _songService.CreateSongAsync(createSong.ToModel(), cancellationToken);
            return CreatedAtRoute(nameof(GetSongByIdAsync), new { id = song.Id }, song.ToResponse());
        }
        catch (SongAlreadyExistsException e)
        {

            return Conflict(new { Message = e.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSongAsync(Guid id, [FromBody] UpdateSongRequest song, CancellationToken cancellationToken)
    {

        try
        {
            await _songService.UpdateSongAsync(song.ToModel(id), cancellationToken);
            return NoContent();
        }
        catch (SongNotFoundException)
        {
            return NotFound();
        }
        catch (SongAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<SongResponse>> PatchSongAsync(Guid id, [FromBody] PatchSongRequest songRequest, CancellationToken cancellationToken)
    {
        try
        {
            var song = await _songService.PatchSongAsync(id, songRequest.Title, songRequest.Artist, songRequest.Album, songRequest.Genre, songRequest.Year, cancellationToken);
            return Ok(song.ToResponse()); // 204
        }
        catch (SongNotFoundException)
        {
            return NotFound(); // 404
        }
        catch (SongAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message }); // 409
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSongAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _songService.DeleteSongAsync(id, cancellationToken);
            return NoContent(); //204
        }
        catch (SongNotFoundException)
        {
            return NotFound(); //404
        }
    }
}