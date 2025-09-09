using Livetta.Application.Contracts;
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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Livetta API",
        Version = "v1",
        Description = "An Livetta Web API with Swagger"
    });
});

builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();

builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();

builder.Services.AddScoped<IResidencesRepository, ResidencesRepository>();

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
    
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync().ConfigureAwait(false);