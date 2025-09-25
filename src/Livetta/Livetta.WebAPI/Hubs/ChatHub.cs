using System.Security.Claims;
using Livetta.Domain.Repositories;
using Livetta.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Livetta.WebAPI.Hubs;

[Authorize]
public sealed class ChatHub(IChatRepository chatRepository) : Hub<IClientProxy>
{
    public override async Task OnConnectedAsync()
    {
        var userIdStr = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (!Guid.TryParse(userIdStr, out var residentId)) 
            return;

        var chatList = await chatRepository.ToListAsync(chatRepository.Get(new(ResidentId: residentId)));

        foreach (var chat in chatList)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, SignalRGroupBuilder.GetChatGroup(chat.Id));
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return Task.CompletedTask;
    }
}