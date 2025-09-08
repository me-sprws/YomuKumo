using Livetta.Application.Contracts;
using Livetta.Application.DTO.Residents;
using Microsoft.AspNetCore.Mvc;

namespace Livetta.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ResidentController(IResidentService residentService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(ResidentCreateDto request, CancellationToken ctk = default)
    {
        return Ok(await residentService.Create(request, ctk));
    }
    
    [HttpGet]
    public async Task<IActionResult> Find([FromQuery] Guid id, CancellationToken ctk = default)
    {
        return await residentService.Find(id, ctk) is { } resident
            ? Ok(resident)
            : NotFound();
    }
}