namespace Livetta.Domain.Contracts;

public interface IHasKey<TId>
{
    TId Id { get; set; }
}