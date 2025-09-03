using System.Runtime.Serialization;

namespace PokemonApi.Dtos;

[DataContract(Name = "UpdatePokemonDto", Namespace = "http://pokemon-api/pokemon-dto")]

public class UpdatePokemonDto
{

    [DataMember(Name = "Id", Order = 1)]
    public Guid Id { get; set; }

    [DataMember(Name = "Name", Order = 2)]
    public string Name { get; set; }

    [DataMember(Name = "Type", Order = 3)]
    public string Type { get; set; }
    
        [DataMember(Name = "Stats", Order = 4)]
    public StatsDto Stats { get; set; }



}