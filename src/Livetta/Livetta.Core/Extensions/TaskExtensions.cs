using DotNext;

namespace Livetta.Core.Extensions;

public static class TaskExtensions
{
    public static async Task<Result<Unit>> Try(this Task task, bool configureCaptureContext = false)
    {
        try
        {
            await task.ConfigureAwait(configureCaptureContext);
            return new(Unit.Default);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
    
    public static async Task<Result<T>> Try<T>(this Task<T> task, bool configureCaptureContext = false)
    {
        try
        {
            var result = await task.ConfigureAwait(configureCaptureContext);
            return new(result);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}