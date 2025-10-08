using System.ComponentModel.DataAnnotations;

namespace SongsApi.Dtos;

public class CreateSongRequest
{
    [Required]
    public string Title { get; set; }

    public string Artist { get; set; }

    public string Album { get; set; }

    public string Genre { get; set; }

    public int Year { get; set; }
}