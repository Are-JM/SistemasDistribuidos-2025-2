using PokedexApi.Gateways;
using PokedexApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonGateway, PokemonGateway>();


var app = builder.Build();

//levantar URL para ver la UI
app.UseSwagger();
app.UseSwaggerUI();

//al hacer petición http redirecciona a https
app.UseHttpsRedirection();
app.MapControllers();

app.Run();