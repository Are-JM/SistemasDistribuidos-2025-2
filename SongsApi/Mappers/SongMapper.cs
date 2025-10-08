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

    public static SongResponse ToResponse(this Song song)
    {
        return new SongResponse
        {
            Id = song.Id,
            Title = song.Title,
            Artist = song.Artist,
            Album = song.Album,
            Genre = song.Genre,
            Year = song.Year
        };
    }

    public static IList<Song> ToModel(this IList<CancionResponseDto> songResponseDtos)
    {
        return songResponseDtos.Select(s => s.ToModel()).ToList();

    }

    public static IList<SongResponse> ToResponse(this IList<Song> songs)
    {
        return songs.Select(s => s.ToResponse()).ToList();
    }

    public static Song ToModel(this CreateSongRequest createSongRequest)
    {
        return new Song
        {
            Title = createSongRequest.Title,
            Artist = createSongRequest.Artist,
            Album = createSongRequest.Album,
            Genre = createSongRequest.Genre,
            Year = createSongRequest.Year
        };
    }

    public static CreateCancionDto ToRequest(this Song song)
    {
        return new CreateCancionDto
        {
            Title = song.Title,
            Artist = song.Artist,
            Album = song.Album,
            Genre = song.Genre,
            Year = song.Year
        };
    }

    public static PaginatedResponse ToPagedResponse(this PagedResult<Song> pagedResult)
    {
        return new PaginatedResponse
        {
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize,
            TotalRecords = pagedResult.TotalRecords,
            TotalPages = pagedResult.TotalPages,
            Data = pagedResult.Data.ToResponse()
        };
    }

    public static PagedResult<Song> ToPagedResult(this PagedResponseDto pagedDto)
    {
        if (pagedDto == null)
        {
            return new PagedResult<Song>
            {
                TotalRecords = 0,
                PageNumber = 1,
                PageSize = 0,
                Data = new List<Song>()
            };
        }

        return new PagedResult<Song>
        {
            PageNumber = pagedDto.PageNumber,
            PageSize = pagedDto.PageSize,
            TotalRecords = pagedDto.TotalRecords,
            Data = pagedDto.Data?.ToModel()
        };
    }

    public static UpdateSongDto ToUpdateRequest(this Song song)
    {
        return new UpdateSongDto
        {
            Id = song.Id,
            Title = song.Title,
            Artist = song.Artist,
            Album = song.Album,
            Genre = song.Genre,
            Year = song.Year
        };
    }

    public static Song ToModel(this UpdateSongRequest song, Guid id)
    {
        return new Song
        {
            Id = id,
            Title = song.Title,
            Artist = song.Artist,
            Album = song.Album,
            Genre = song.Genre,
            Year = song.Year
        };
    }
}