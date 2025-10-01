using System.Security.Claims;
using Livetta.Application.Authorization;
using Livetta.Application.Contracts;
using Livetta.Application.DTO.Chats;
using Livetta.Application.DTO.Messages;
using Livetta.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livetta.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public sealed class ChatsController(
    IChatService chatService,
    IMessageService messageService,
    IAuthorizationService authorizationService
) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateChat(ChatCreateDto request)
    {
        return Ok(await chatService.CreateAsync(GetUserId(), request));
    }
    
    [HttpDelete("{chatId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteChat(Guid chatId)
    {
        if (!await RequireChatMemberAsync(chatId, isOwner: true))
            return Forbid();

        await chatService.DeleteAsync(chatId);
        return Ok();
    }
    
    [HttpPost("{chatId:guid}/members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateChatMember(Guid chatId, ChatMemberCreateDto request)
    {
        if (!await RequireChatMemberAsync(chatId, isOwner: true))
            return Forbid();
        
        await chatService.CreateChatMemberAsync(chatId, request);
        return Ok();
    }
    
    [HttpDelete("{chatId:guid}/members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteChatMember(Guid chatId, ChatMemberDeleteDto request)
    {
        if (!await RequireChatMemberAsync(chatId, isOwner: true))
            return Forbid();
        
        await chatService.DeleteChatMemberAsync(chatId, request);
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUserChats()
    {
        return Ok(await chatService.GetAllAsync(GetUserId()));
    }
    
    [HttpGet("{chatId:guid}/messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChatMessages(Guid chatId, int take, int offset)
    {
        if (!await RequireChatMemberAsync(chatId))
            return Forbid();
        
        return Ok(await messageService.GetMessagesAsync(chatId, take, offset));
    }
    
    [HttpPost("{chatId:guid}/messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateChatMessage(Guid chatId, MessageCreateDto request)
    {
        if (!await RequireChatMemberAsync(chatId))
            return Forbid();
        
        return Ok(await messageService.CreateAsync(chatId, GetUserId(), request));
    }

    Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    async Task<bool> RequireChatMemberAsync(Guid chatId, bool isOwner = false) =>
        (await authorizationService.AuthorizeChatMemberAsync(User, new(chatId, isOwner))).Succeeded;
}
