using SingerApi.Models;

namespace SingerApi.Repositories;

public interface ISingerRepository
{
    Task<Singer?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Singer> CreateAsync(Singer singer, CancellationToken cancellationToken);
    Task<IEnumerable<Singer>> GetByNameAsync(string name, CancellationToken cancellationToken);

}