namespace PokedexApi.Dtos;

public class UpdatePokemonRequest
{
    public string Name { get; set; }
    public string Type { get; set; }
    public StatsRequest Stats { get; set; }

}
