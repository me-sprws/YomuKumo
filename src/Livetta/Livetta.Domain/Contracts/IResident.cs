namespace Livetta.Domain.Contracts;

/// <summary>
/// Житель дома.
/// </summary>
public interface IResident : IEntity
{
    string FullName { get; }
    IContacts Contacts { get; }
    
    /// <summary>
    /// Владеет или проживает в апартаментах.
    /// </summary>
    IReadOnlyCollection<IApartment> Apartments { get; }

    /// <summary>
    /// Добавить новые апартаменты.
    /// </summary>
    /// <param name="apartment"></param>
    void AddApartment(IApartment apartment);
    
    /// <summary>
    /// Удалить апартаменты.
    /// </summary>
    bool RemoveApartment(IApartment apartment);
}