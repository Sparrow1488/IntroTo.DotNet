using Microsoft.AspNetCore.Http;

namespace CleanServices.API.Infrastructure.Exceptions.StatusCode;

public sealed class ValidationException : ApiException, IStatusCodeException
{
    public ValidationException(string message, IDictionary<string, string[]> errors) : base(message)
    {
        AddErrors(errors);
    }

    public ValidationException(
        string message, 
        IDictionary<string, string[]> errors, 
        Exception innerException
    ) : base(message, innerException)
    {
        AddErrors(errors);
    }
    
    public int StatusCode => StatusCodes.Status400BadRequest;

    private void AddErrors(IDictionary<string, string[]> errors)
    {
        foreach (var (key, value) in errors)
            Data.Add(key, value);
    }
}