using System.Security.Claims;
using Livetta.Domain.Entities.Messaging;
using Livetta.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Livetta.WebAPI.Hubs;

[Authorize]
public sealed class ChatHub(IChatRepository chatRepository) : Hub<IClientProxy>
{
    readonly Dictionary<string, Guid> _connectionToResidentMap = new();
    
    public override async Task OnConnectedAsync()
    {
        var userIdStr = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (!Guid.TryParse(userIdStr, out var residentId)) 
            return;

        _connectionToResidentMap[Context.ConnectionId] = residentId;

        var chatList = await chatRepository.ToListAsync(chatRepository.Get(new(ResidentId: residentId)));

        foreach (var chat in chatList)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GetChatGroup(chat));
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connectionToResidentMap.Remove(Context.ConnectionId);
        return Task.CompletedTask;
    }

    static string GetChatGroup(Chat chat) => $"chat-{chat.Id}";
}