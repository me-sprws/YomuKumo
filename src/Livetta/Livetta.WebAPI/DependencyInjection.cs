using System.Text;
using Livetta.Application.Contracts;
using Livetta.Application.Services;
using Livetta.Domain.Repositories;
using Livetta.Infrastructure.Persistence;
using Livetta.Infrastructure.Persistence.Repositories;
using Livetta.Security.Policies;
using Livetta.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace Livetta.WebAPI;

internal static class DependencyInjection
{
    public static WebApplicationBuilder AddLivettaAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();

                var jwtKey = builder.Configuration.GetValue<string>("Jwt:SecretKey")!;
        
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,     // Проверка издателя
                    ValidateAudience = false,   // Проверка получателя
                    ValidateLifetime = true,    // Проверка срока жизни
                    ValidateIssuerSigningKey = true, // Проверка подписи
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

        return builder;
    }

    public static WebApplicationBuilder AddLivettaAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(
                LivettaPolicy.Messaging.CanCreateChats,
                p => p.RequireAuthenticatedUser());

        return builder;
    }

    public static WebApplicationBuilder AddEntityFrameworkCore(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LivettaDbContext>(options =>
            options.UseNpgsql(builder.Configuration["Database:ConnectionString"])
                .UseSnakeCaseNamingConvention());

        return builder;
    }

    public static WebApplicationBuilder AddLivettaSwaggerGen(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "Livetta API",
                Version = "v1",
                Description = "An Livetta Web API with Swagger"
            });
    
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Введите JWT токен в формате: Bearer {ваш токен}"
            });

            c.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return builder;
    }

    public static WebApplicationBuilder AddLivettaServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IResidentService, ResidentService>();
        builder.Services.AddScoped<IResidentRepository, ResidentRepository>();

        builder.Services.AddScoped<IApartmentService, ApartmentService>();
        builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();

        builder.Services.AddScoped<IChatService, ChatService>();
        builder.Services.AddScoped<IChatRepository, ChatRepository>();

        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();

        builder.Services.AddScoped<IResidencesRepository, ResidencesRepository>();

        builder.Services.AddScoped<INotificationService, SignalRNotificationService>();

        return builder;
    }

    public static WebApplicationBuilder AddLivettaEndpoints(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddNewtonsoftJson(
                options =>
                {
                    var serializer = options.SerializerSettings;
            
                    serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    // serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }
            );

        builder.Services.AddSignalR();

        return builder;
    }
}