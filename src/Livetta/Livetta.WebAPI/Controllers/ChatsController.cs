using Livetta.Application.Contracts;
using Livetta.Application.DTO.Chats;
using Livetta.Application.DTO.Messages;
using Livetta.Security.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livetta.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public sealed class ChatsController(
    IChatService chatService,
    IMessageService messageService
) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = LivettaPolicy.Messaging.CanCreateChats)]
    public async Task<IActionResult> Post(ChatCreateDto request)
    {
        return Ok(await chatService.CreateAsync(GetRequestResidentId(), request));
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await chatService.GetAllAsync(GetRequestResidentId()));
    }
    
    [HttpGet("{chatId:guid}/messages")]
    [Authorize]
    public async Task<IActionResult> GetMessages(Guid chatId, int take, int offset)
    {
        // TODO: проверка относится ли пользователь к чату
        return Ok(await messageService.GetMessagesAsync(chatId, take, offset));
    }
    
    [HttpPost("{chatId:guid}/messages")]
    [Authorize]
    public async Task<IActionResult> CreateMessage(Guid chatId, MessageCreateDto request)
    {
        // TODO: проверка относится ли пользователь к чату
        return Ok(await messageService.CreateAsync(chatId, GetRequestResidentId(), request));
    }

    static Guid GetRequestResidentId()
    {
        // TODO: из UserIdentity берем ID пользователя и используем, но пока делаем вот так:
        return Guid.Parse("01992b6c-fb6f-7ca6-a0fb-9a2eeff0bdeb");
    }
}
