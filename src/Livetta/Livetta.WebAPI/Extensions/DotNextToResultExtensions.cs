using DotNext;

namespace Livetta.WebAPI.Extensions;

public static class DotNextToResultExtensions
{
    /// <summary>
    /// Конвертирует результат в Ok (200), иначе возвращает пустой ответ Empty (200).
    /// </summary>
    public static IResult OrEmpty<T>(this Result<T> result)
    {
        return result.Convert(Results.Ok).OrInvoke(_ => Results.Empty);
    }
    
    /// <summary>
    /// Конвертирует результат в Ok (200), иначе возвращает BadRequest (400).
    /// </summary>
    public static IResult OrBadRequest<T>(this Result<T> result)
    {
        return result.Convert(Results.Ok).OrInvoke(_ => Results.BadRequest());
    }
    
    /// <summary>
    /// Конвертирует результат в Ok (200), иначе возвращает NotFound (404).
    /// </summary>
    public static IResult OrNotFound<T>(this Optional<T> optional)
    {
        return optional.Convert(Results.Ok).OrInvoke(() => Results.NotFound());
    }
}