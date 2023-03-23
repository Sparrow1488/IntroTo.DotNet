using CleanServices.API.Contracts.Error.Responses;
using Microsoft.AspNetCore.Http;

namespace CleanServices.API.Infrastructure.Exceptions.StatusCode;

public class BadRequestException : ApiException, IStatusCodeException, IDetailsException
{
    public BadRequestException() { }

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(string message, Exception innerException) : base(message, innerException) { }

    public int StatusCode => StatusCodes.Status400BadRequest;
    public IList<ErrorDetails> Details { get; }
}