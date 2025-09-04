using Livetta.Application.Contracts;
using Livetta.Application.DTO.Residents;
using Livetta.Application.Services;
using Livetta.Domain.Repositories;
using Livetta.Infrastructure.Persistence;
using Livetta.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<LivettaDbContext>(options =>
    options.UseNpgsql(builder.Configuration["Database:ConnectionString"])
           .UseSnakeCaseNamingConvention());

builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();

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

app.MapGet("/resident/{id:guid}", (IResidentService service, Guid id, CancellationToken ctk = default) => 
    service.GetById(id, ctk));

app.MapGet("/resident", (IResidentService service, CancellationToken ctk = default) => 
    service.GetAll(ctk));

app.MapPost("/resident", (IResidentService service, ResidentCreateDto create, CancellationToken ctk = default) => 
    service.Create(create, ctk));

app.Run();