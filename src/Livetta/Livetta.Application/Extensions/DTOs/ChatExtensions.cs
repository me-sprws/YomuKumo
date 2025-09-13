using Livetta.Application.DTO.Chats;
using Livetta.Domain.Entities.Messaging;

namespace Livetta.Application.Extensions.DTOs;

public static class ChatExtensions
{
    public static ChatReadDto ToDto(this Chat chat)
    {
        return new(
            chat.Id, 
            chat.Residents.Select(x => x.Resident).Select(x => x.ToShortDto()).ToArray(), 
            chat.Messages.FirstOrDefault()?.ToDto());
    }
}