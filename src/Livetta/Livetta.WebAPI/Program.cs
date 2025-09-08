using Livetta.Application.Contracts;
using Livetta.Application.DTO.Residents;
using Livetta.Application.Services;
using Livetta.Domain.Repositories;
using Livetta.Infrastructure.Persistence;
using Livetta.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log.log", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<LivettaDbContext>(options =>
    options.UseNpgsql(builder.Configuration["Database:ConnectionString"])
           .UseSnakeCaseNamingConvention());

builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();

builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(
        options =>
        {
            var serializer = options.SerializerSettings;
            
            serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync().ConfigureAwait(false);