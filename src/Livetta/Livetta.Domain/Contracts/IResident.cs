namespace Livetta.Domain.Contracts;

/// <summary>
/// Житель дома.
/// </summary>
public interface IResident
{
    string FullName { get; }
    IContacts Contacts { get; }
}