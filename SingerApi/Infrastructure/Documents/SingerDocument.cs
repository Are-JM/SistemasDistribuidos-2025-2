using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace SingerApi.Infrastructure.Documents;

public class SingerDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("debut_year")]
    public int debut_year { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}