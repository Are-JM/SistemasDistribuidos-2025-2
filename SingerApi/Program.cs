using SingerApi.Services;
using MongoDB.Driver;
using SingerApi.Infrastructure;
using SingerApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<ISingerRepository, SingerRepository>();

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = builder.Configuration.GetSection("MongoDB").Get<MongoDBSettings>();
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<SingerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
