using Livetta.Application.Contracts;
using Livetta.Application.DTO.Residents;
using Microsoft.AspNetCore.Mvc;

namespace Livetta.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ResidentsController(IResidentService residentService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateResident(ResidentCreateDto request, CancellationToken ctk = default)
    {
        return Ok(await residentService.CreateAsync(request, ctk));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindResident(Guid id, CancellationToken ctk = default)
    {
        return await residentService.FindAsync(id, ctk) is { } resident
            ? Ok(resident)
            : NotFound();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllResidents(CancellationToken ctk = default)
    {
        return Ok(await residentService.GetAllAsync(ctk));
    }
}