using System.ServiceModel;
using CancionesApi.Dtos;

namespace CancionesApi.Validators;

public static class PokemonValidator
{
    public static CreateCancionDto ValidateTitle(this CreateCancionDto cancion) =>
    string.IsNullOrEmpty(cancion.Title) ? throw new FaultException("Song title is required") : cancion;

    public static CreateCancionDto ValidateArtist(this CreateCancionDto cancion) =>
    string.IsNullOrEmpty(cancion.Artist) ? throw new FaultException("Song artist is required") : cancion;

    public static CreateCancionDto ValidateAlbum(this CreateCancionDto cancion) =>
    string.IsNullOrEmpty(cancion.Album) ? throw new FaultException("Song album name is required") : cancion;

    public static CreateCancionDto ValidateGenre(this CreateCancionDto cancion) =>
    string.IsNullOrEmpty(cancion.Genre) ? throw new FaultException("Song genre is required") : cancion;
}