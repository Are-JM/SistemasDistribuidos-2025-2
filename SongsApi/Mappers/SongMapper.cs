using Microsoft.AspNetCore.StaticAssets;
using SongsApi.Dtos;
using SongsApi.Infrastructure.Soap.Contracts;
using SongsApi.Models;

namespace SongsApi.Mappers;

public static class SongMapper
{
    public static Song ToModel(this CancionResponseDto songResponseDto)
    {
        return new Song
        {
            Id = songResponseDto.Id,
            Title = songResponseDto.Title,
            Artist = songResponseDto.Artist,
            Album = songResponseDto.Album,
            Genre = songResponseDto.Genre,
            Year = songResponseDto.Year
        };
    }


    public static CancionResponseDto ToResponse(this Song song)
    {
        return new CancionResponseDto
        {
            Id = song.Id,
            Title = song.Title,
            Artist = song.Artist,
            Album = song.Album,
            Genre = song.Genre,
            Year = song.Year
        };
    }


}