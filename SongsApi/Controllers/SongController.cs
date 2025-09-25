using System.ServiceModel.Channels;
using Microsoft.AspNetCore.Mvc;
using SongsApi.Dtos;
using SongsApi.Services;
using SongsApi.Mappers;

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
}