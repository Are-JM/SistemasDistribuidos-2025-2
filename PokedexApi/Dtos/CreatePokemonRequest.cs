using System.ComponentModel.DataAnnotations;

namespace PokedexApi.Dtos;

public class CreatePokemonRequest
{
    [Required]
    public string Name { get; set; }
    [MinLength(3)]
    public string Type { get; set; }
    public int Level { get; set; }

    public StatsRequest Stats { get; set; }
}

public class StatsRequest
{
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
}