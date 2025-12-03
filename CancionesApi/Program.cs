using Microsoft.EntityFrameworkCore;
using CancionesApi.Infrastructure;
using CancionesApi.Services;
using SoapCore;
using CancionesApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSoapCore();
builder.Services.AddScoped<ICancionesService, CancionesService>();
builder.Services.AddScoped<ICancionRepository, CancionRepository>();
builder.Services.AddDbContext<RelationalDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();
app.UseSoapEndpoint<ICancionesService>("/CancionesService.svc", new SoapEncoderOptions());
app.Run();
