using Google.Protobuf.WellKnownTypes;
using SingerApi.Infrastructure.Documents;
using SingerApi.Models;

namespace SingerApi.Mappers;

public static class SingerMapper{
    public static Singer ToDomain(this SingerDocument document)
    {
        if (document is null)
        {
            return null;
        }
        return new Singer
        {
            Id = document.Id,
            Name = document.Name,
            DebutYear = document.debut_year,
            CreatedAt = document.CreatedAt
        };
    }
    public static SingerResponse ToResponse(this Singer singer)
    {
        return new SingerResponse
        {
            Id = singer.Id,
            Name = singer.Name,
            DebutYear = singer.DebutYear,
            CreatedAt = Timestamp.FromDateTime(singer.CreatedAt.ToUniversalTime())
        };
    }

    public static Singer ToModel(this CreateSingerRequest request)
    {
        return new Singer
        {
            Name = request.Name,
            DebutYear = request.DebutYear
        };
    }

    public static SingerDocument ToDocument(this Singer singer)
    {
        return new SingerDocument
        {
            Id = singer.Id,
            Name = singer.Name,
            debut_year = singer.DebutYear,
            CreatedAt = singer.CreatedAt
        };
    }
}