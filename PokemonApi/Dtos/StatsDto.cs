using System.Runtime.Serialization;

namespace PokemonApi.Dtos;

[DataContract(Name = "StatsDto", Namespace = "http://pokemon-api/pokemon-service")]
public class StatsDto
{
    [DataMember(Name = "Attack", Order = 1)]
    public int Attack { get; set; }

    [DataMember(Name = "Defense", Order = 2)]
    public int Defense { get; set; }

    [DataMember(Name = "Speed", Order = 3)]
    public int Speed { get; set; }
    
    [DataMember(Name = "Stamina", Order = 4)]
    public int Stamina { get; set; }
}