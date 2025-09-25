using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Livetta.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Livetta.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public sealed class IdentityController(
    IResidentRepository residentRepository, 
    IConfiguration configuration
) : ControllerBase
{
    [HttpPost("{residentId:guid}")]
    public async Task<IActionResult> Login(Guid residentId)
    {
        var get = residentRepository.Get(new(IncludeContacts: true));
        
        var resident = await residentRepository.FirstOrDefaultAsync(get, residentId);

        if (resident is null)
            return BadRequest();
        
        var jwtKey = configuration.GetValue<string>("Jwt:SecretKey")!;
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, resident.Contacts.Email),
            new Claim(ClaimTypes.NameIdentifier, resident.Id.ToString())
        };
    
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "Livetta",
            audience: "Livetta",
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);
        
        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}