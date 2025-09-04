using Microsoft.AspNetCore.StaticAssets;
using CancionesApi.Dtos;
using CancionesApi.Infrastructure.Entities;
using CancionesApi.Models;

namespace CancionesApi.Mappers;

public static class CancionMapper
{
    public static Cancion ToModel(this CancionEntity cancionEntity)
    {
        if (cancionEntity is null)
        {
            return null;
        }

        return new Cancion
        {
            Id = cancionEntity.Id,
            Title = cancionEntity.Title,
            Artist = cancionEntity.Artist,
            Album = cancionEntity.Album,
            Genre = cancionEntity.Genre,
            Year = cancionEntity.Year
        };
    }

    public static CancionEntity ToEntity(this Cancion cancion)
    {
        if (cancion is null)
        {
            return null;
        }

        return new CancionEntity
        {
            Id = cancion.Id,
            Title = cancion.Title,
            Artist = cancion.Artist,
            Album = cancion.Album,
            Genre = cancion.Genre,
            Year = cancion.Year
        };
    }

    public static Cancion ToModel(this CreateCancionDto requestCancionDto)
    {
        return new Cancion
        {
            Title = requestCancionDto.Title,
            Artist = requestCancionDto.Artist,
            Album = requestCancionDto.Album,
            Genre = requestCancionDto.Genre,
            Year = requestCancionDto.Year
        };
    }

    public static CancionResponseDto ToResponseDto(this Cancion cancion)
    {
        return new CancionResponseDto
        {
            Id = cancion.Id,
            Title = cancion.Title,
            Artist = cancion.Artist,
            Album = cancion.Album,
            Genre = cancion.Genre,
            Year = cancion.Year
        };
    }
}