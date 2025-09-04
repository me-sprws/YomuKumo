using Livetta.Domain.ValueObjects;

namespace Livetta.Domain.Contracts;

/// <summary>
/// Полная контактная информация.
/// </summary>
public interface IContacts
{
    string? FirstName { get; }
    string? LastName { get; }
    Phone? Phone { get; }
}