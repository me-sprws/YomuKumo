using System.Security.Claims;
using Livetta.Application.Contracts;
using Livetta.Application.DTO.Chats;
using Livetta.Application.DTO.Messages;
using Livetta.Security.Policies;
using Livetta.WebAPI.Extensions;
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
    
    [HttpDelete("{chatId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteChat(Guid chatId)
    {
        if (!await RequireChatMemberAsync(chatId, isOwner: true))
            return Forbid();

        await chatService.DeleteAsync(chatId);
        return Ok();
    }
    
    [HttpPost("{chatId:guid}/members")]
    [Authorize]
    public async Task<IActionResult> CreateChatMember(Guid chatId, ChatMemberCreateDto request)
    {
        if (!await RequireChatMemberAsync(chatId, isOwner: true))
            return Forbid();
        
        await chatService.CreateChatMemberAsync(chatId, request);
        return Ok();
    }
    
    [HttpDelete("{chatId:guid}/members")]
    [Authorize]
    public async Task<IActionResult> DeleteChatMember(Guid chatId, ChatMemberDeleteDto request)
    {
        if (!await RequireChatMemberAsync(chatId, isOwner: true))
            return Forbid();
        
        await chatService.DeleteChatMemberAsync(chatId, request);
        return Ok();
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

    async Task<bool> RequireChatMemberAsync(Guid chatId, bool isOwner = false) =>
        (await authorizationService.AuthorizeChatMemberAsync(User, new(chatId, isOwner))).Succeeded;
}
