namespace Livetta.Domain.Contracts;

/// <summary>
/// Апартаменты.
/// </summary>
public interface IApartment : IEntity
{
    /// <summary>
    /// Адрес.
    /// </summary>
    string Address { get; }
    
    /// <summary>
    /// Количество комнат.
    /// </summary>
    int Room { get; }
    
    /// <summary>
    /// Этаж.
    /// </summary>
    int Floor { get; }
    
    /// <summary>
    /// Площадь.
    /// </summary>
    double Area { get; }
    
    /// <summary>
    /// Жильцы.
    /// </summary>
    IReadOnlyCollection<IResident> Residents { get; }

    /// <summary>
    /// Добавить нового жильца.
    /// </summary>
    void AddResident(IResident resident);
    
    /// <summary>
    /// Удалить жильцов.
    /// </summary>
    int RemoveResidentAll(Predicate<IResident> match);
}