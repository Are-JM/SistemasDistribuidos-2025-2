using System.ServiceModel;
using CancionesApi.Dtos;
using CancionesApi.Infrastructure;
using CancionesApi.Mappers;
using CancionesApi.Repositories;
using CancionesApi.Validators;

namespace CancionesApi.Services;

public class CancionesService : ICancionesService
{
    private readonly ICancionRepository _cancionRepository;

    public CancionesService(ICancionRepository cancionRepository)
    {
        _cancionRepository = cancionRepository;
    }

    public async Task<CancionResponseDto> CreateCancion(CreateCancionDto cancionRequest, CancellationToken cancellationToken)
    {
        cancionRequest.ValidateTitle().ValidateArtist().ValidateAlbum().ValidateGenre();

        if (await CancionAlreadyExist(cancionRequest.Title, cancionRequest.Artist, cancellationToken))
        {
            throw new FaultException("Song already exists");
        }

        var cancion = await _cancionRepository.CreateCancionAsync(cancionRequest.ToModel(), cancellationToken);
        return cancion.ToResponseDto();

    }

    private async Task<bool> CancionAlreadyExist(string title, string artist, CancellationToken cancellationToken)
    {
        var cancion = await _cancionRepository.GetByTitleAndArtistAsync(title, artist, cancellationToken);
        return cancion != null;
    }
}