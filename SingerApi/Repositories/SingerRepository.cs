using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SingerApi.Infrastructure;
using SingerApi.Infrastructure.Documents;
using SingerApi.Mappers;
using SingerApi.Models;

namespace SingerApi.Repositories;

public class SingerRepository : ISingerRepository
{
    private readonly IMongoCollection<SingerDocument> _singerCollection;

    public SingerRepository(IMongoDatabase database, IOptions<MongoDBSettings> settings)
    {
        _singerCollection = database.GetCollection<SingerDocument>(settings.Value.SingersCollectionName);

    }

    public async Task<Singer> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var singer = await _singerCollection.Find(t => t.Id == id).FirstOrDefaultAsync(cancellationToken);
        return singer.ToDomain();
    }

    public async Task<Singer> CreateAsync(Singer singer, CancellationToken cancellationToken)
    {
        singer.CreatedAt = DateTime.UtcNow;
        var singerToCreate = singer.ToDocument();
        await _singerCollection.InsertOneAsync(singerToCreate, cancellationToken: cancellationToken);
        return singerToCreate.ToDomain();
    }

    public async Task<IEnumerable<Singer>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var singers = await _singerCollection.Find(s => s.Name.Contains(name)).ToListAsync(cancellationToken);
        return singers.Select(s => s.ToDomain());
    }
}