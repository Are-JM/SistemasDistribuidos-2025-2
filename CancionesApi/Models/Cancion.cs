using CancionesApi.Dtos;

namespace CancionesApi.Models;

public class Cancion
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Artist { get; set; }
    public required string Album { get; set; }
    public required string Genre { get; set; }
    public int Year { get; set; }
}