using Livetta.Application.Contracts;
using Livetta.Application.DTO.Apartments;
using Microsoft.AspNetCore.Mvc;

namespace Livetta.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ApartmentController(IApartmentService apartmentService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Post(ApartmentCreateDto request, CancellationToken ctk = default)
    {
        return Ok(await apartmentService.CreateAsync(request, ctk));
    }
    
    [HttpPut("{apartmentId:guid}/{residentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Put(Guid apartmentId, Guid residentId, ResidentAssignDto request, CancellationToken ctk = default)
    {
        await apartmentService.AssignAsync(residentId, apartmentId, request, ctk);
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ctk = default)
    {
        return Ok(await apartmentService.GetAllAsync(ctk));
    }
}