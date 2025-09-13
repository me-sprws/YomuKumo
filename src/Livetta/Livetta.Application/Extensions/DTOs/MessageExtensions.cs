using Livetta.Application.DTO.Messages;
using Livetta.Domain.Entities.Messaging;

namespace Livetta.Application.Extensions.DTOs;

public static class MessageExtensions
{
    public static MessageReadDto ToDto(this Message message)
    {
        return new(message.Id, message.ChatId, message.ResidentId, message.Text);
    }
}