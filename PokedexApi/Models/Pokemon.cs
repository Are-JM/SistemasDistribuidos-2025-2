namespace PokedexApi.Models;

using PokedexApi.Models;

public class Pokemon
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public int Level { get; set; }
    public Stats stats { get; set; }
}

public class Stats
{

    public int Attack { get; set; }

    public int Defense { get; set; }

    public int Speed { get; set; }
}