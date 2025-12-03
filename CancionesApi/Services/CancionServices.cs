using System.ServiceModel;
using CancionesApi.Dtos;
using CancionesApi.Infrastructure;
using CancionesApi.Mappers;
using CancionesApi.Models;
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
        return CancionExists(cancion);
    }

    public async Task<CancionResponseDto> GetCancionById(Guid id, CancellationToken cancellationToken)
    {
        var cancion = await _cancionRepository.GetCancionByIdAsync(id, cancellationToken);
        return CancionExists(cancion) ? cancion.ToResponseDto() : throw new FaultException("Song doesn't exist");
    }

    private static bool CancionExists(Cancion? cancion)
    {
        return cancion is not null;
    }

    public async Task<DeleteCancionResponseDto> DeleteCancionAsync(Guid id, CancellationToken cancellationToken)
    {
        var cancion = await _cancionRepository.GetCancionByIdAsync(id, cancellationToken);
        if (!CancionExists(cancion))
        {
            throw new FaultException(reason: "Song not found");
        }
        await _cancionRepository.DeleteCancionAsync(cancion, cancellationToken);
        return new DeleteCancionResponseDto { Success = true };
    }

    public async Task<IList<CancionResponseDto>> GetSongsByArtist(string artist, CancellationToken cancellationToken)
    {
        var songs = await _cancionRepository.GetSongsByArtist(artist, cancellationToken);
        return songs.ToResponseDto();
    }

    public async Task<CancionResponseDto> UpdateSong(UpdateSongDto songToUpdate, CancellationToken cancellationToken)
    {
        var song = await _cancionRepository.GetCancionByIdAsync(songToUpdate.Id, cancellationToken);
        if (!CancionExists(song))
        {
            throw new FaultException(reason: "Song not found");
        }

        if (!await IsSongAllowedToBeUpdated(songToUpdate, cancellationToken))
        {
            throw new FaultException("Another song by the same title and artist already exists");
        }

        song.Title = songToUpdate.Title;
        song.Artist = songToUpdate.Artist;
        song.Album = songToUpdate.Album;
        song.Genre = songToUpdate.Genre;

        await _cancionRepository.UpdateSongAsync(song, cancellationToken);
        return song.ToResponseDto();
    }

    private async Task<bool> IsSongAllowedToBeUpdated(UpdateSongDto songToUpdate, CancellationToken cancellationToken)
    {
        var duplicatedSong = await _cancionRepository.GetByTitleAndArtistAsync(songToUpdate.Title, songToUpdate.Artist, cancellationToken);

        if (duplicatedSong is null)
        {
            return true;
        }

        if (duplicatedSong.Id == songToUpdate.Id)
        {
            return true;
        }

        return false;
    }

    public async Task<PagedResponseDto> GetSongs(QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        return await _cancionRepository.GetSongsAsync(queryParameters, cancellationToken);
    }
}