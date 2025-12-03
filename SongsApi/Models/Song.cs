using SongsApi.Dtos;

namespace SongsApi.Models;

public class Song
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Artist { get; set; }
    public required string Album { get; set; }
    public required string Genre { get; set; }
    public int Year { get; set; }
}