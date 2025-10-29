namespace PokedexApi.Dtos;

public class PaginatedResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public IList<PokemonResponse> Data { get; set; } = new List<PokemonResponse>();
}