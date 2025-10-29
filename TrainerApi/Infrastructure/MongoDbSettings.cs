namespace TrainerApi.Infrastructure;

public class MongoDBSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string TrainersCollectionName { get; set; } = null!;
}