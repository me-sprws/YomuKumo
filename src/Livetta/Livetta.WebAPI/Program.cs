using Livetta.WebAPI;
using Livetta.WebAPI.Hubs;
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

builder.AddLivettaAuthentication()
       .AddLivettaAuthorization()
       .AddEntityFrameworkCore()
       .AddLivettaSwaggerGen()
       .AddLivettaEndpoints()
       .AddLivettaServices();

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
app.MapHub<ChatHub>("/hub/chat");

await app.RunAsync();