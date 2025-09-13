namespace Livetta.Application.DTO.Messages;

public abstract record MessageReadDtoBase(Guid Id, Guid ChatId, Guid? ResidentId, string Text);

public record MessageReadDto(Guid Id, Guid ChatId, Guid? ResidentId, string Text) 
    : MessageReadDtoBase(Id, ChatId, ResidentId, Text);