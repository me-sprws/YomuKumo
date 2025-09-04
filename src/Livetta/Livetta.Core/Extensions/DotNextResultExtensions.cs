using DotNext;

namespace Livetta.Core.Extensions;

public static class DotNextResultExtensions
{
    /// <summary>
    /// Проверить значение результата, если выполнено успешно.
    /// </summary>
    public static Result<T> Inspect<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccessful)
            action.Invoke(result.Value);

        return result;
    }
    
    /// <summary>
    /// Проверить ошибку результата, если выполнено с ошибкой.
    /// </summary>
    public static Result<T> InspectError<T>(this Result<T> result, Action<Exception> action)
    {
        if (!result.IsSuccessful)
            action.Invoke(result.Error);

        return result;
    }
}