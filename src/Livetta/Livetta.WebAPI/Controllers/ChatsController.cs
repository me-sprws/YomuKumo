using System.Security.Claims;
using Livetta.Application.Contracts;
using Livetta.Application.DTO.Chats;
using Livetta.Application.DTO.Messages;
using Livetta.Security.Policies;
using Livetta.WebAPI.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livetta.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public sealed class ChatsController(
    IChatService chatService,
    IMessageService messageService,
    IAuthorizationService authorizationService
) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = LivettaPolicy.Messaging.CanCreateChats)]
    public async Task<IActionResult> CreateChat(ChatCreateDto request)
    {
        return Ok(await chatService.CreateAsync(GetUserId(), request));
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllChats()
    {
        return Ok(await chatService.GetAllAsync(GetUserId()));
    }
    
    [HttpGet("{chatId:guid}/messages")]
    [Authorize]
    public async Task<IActionResult> GetMessages(Guid chatId, int take, int offset)
    {
        if (!await RequireChatMemberAsync(chatId))
            return Forbid();
        
        return Ok(await messageService.GetMessagesAsync(chatId, take, offset));
    }
    
    [HttpPost("{chatId:guid}/messages")]
    [Authorize]
    public async Task<IActionResult> CreateMessage(Guid chatId, MessageCreateDto request)
    {
        if (!await RequireChatMemberAsync(chatId))
            return Forbid();
        
        return Ok(await messageService.CreateAsync(chatId, GetUserId(), request));
    }

    Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    async Task<bool> RequireChatMemberAsync(Guid chatId) =>
        (await authorizationService.AuthorizeAsync(
            User, 
            new ChatMemberAuthContext(chatId), 
            LivettaPolicy.Messaging.ChatMember)
        ).Succeeded;
}
