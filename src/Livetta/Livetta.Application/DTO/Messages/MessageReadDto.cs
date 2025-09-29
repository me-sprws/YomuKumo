namespace Livetta.Application.DTO.Messages;

public abstract record MessageReadDtoBase(Guid Id, Guid ChatId, Guid? ResidentId, string Text, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt);

public record MessageReadDto(Guid Id, Guid ChatId, Guid? ResidentId, string Text, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt) 
    : MessageReadDtoBase(Id, ChatId, ResidentId, Text, CreatedAt, UpdatedAt);