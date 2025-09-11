using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var jwtKey = builder.Configuration.GetValue<string>("Jwt:SecretKey")
    ?? throw new InvalidOperationException("No secret key found");

builder.Services.AddAuthentication(op =>
    {
        op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(op =>
    {
        op.TokenValidationParameters = new()
        {
            ValidateIssuer = false,     // Проверка издателя
            ValidateAudience = false,   // Проверка получателя
            ValidateLifetime = true,    // Проверка срока жизни
            ValidateIssuerSigningKey = true, // Проверка подписи
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/auth", () =>
{
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, "sample"),
        new Claim(ClaimTypes.Email, "sample@gmail.com")
    };
    
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "Livetta",
        audience: "Livetta",
        claims: claims,
        expires: DateTime.Now.AddMinutes(10),
        signingCredentials: credentials);

    return Results.Ok(new JwtSecurityTokenHandler().WriteToken(token));
});

app.MapGet("/test", (HttpContext ctx) => $"Hello, {ctx.User.FindFirstValue(ClaimTypes.Name)}!")
    .RequireAuthorization(p => 
        p.RequireClaim(ClaimTypes.Name));

app.Run();
