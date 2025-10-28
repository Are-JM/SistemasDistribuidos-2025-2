using PokedexApi.Gateways;
using PokedexApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonGateway, PokemonGateway>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("Authentication:Authority");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidateActor = false,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidAudience = "pokedex-api",
            ValidateIssuerSigningKey = true
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Read", policy => policy.RequireClaim("http://schemas.microsoft.com/identity/claims/scope", 
                    "read"));
        
        options.AddPolicy("Write", policy => policy.RequireClaim("http://schemas.microsoft.com/identity/claims/scope", 
                    "write"));

});

var app = builder.Build();

//levantar URL para ver la UI
app.UseSwagger();
app.UseSwaggerUI();

//al hacer petición http redirecciona a https
app.UseHttpsRedirection();
app.MapControllers();

app.Run();