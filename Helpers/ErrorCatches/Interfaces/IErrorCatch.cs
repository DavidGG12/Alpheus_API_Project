using System.Diagnostics;

namespace Alpheus_API.Helpers.ErrorCatches.Interfaces
{
    public interface IErrorCatch
    {
        T GenInfoError<T>(string message, string? errorMessage, Stopwatch stopWatch) where T: new();
    }
}
