using System.Net;
using CleanServices.API.Contracts.Error.Responses;
using CleanServices.API.Infrastructure.Exceptions.StatusCode;
using FluentValidation.Results;

namespace CleanServices.API.Infrastructure.Extensions;

public static class ExceptionExtensions
{
    public static Exception ToException(this ValidationResult result)
    {
        var errors = result.ToDictionary();
        return new ValidationException("Some validation errors", errors);
    }
    
    public static ErrorResponse ToErrorResponse(this Exception ex)
    {
        var response = new ErrorResponse();
        
        var error = CreateError(ex);
        if (ex.InnerException is not null)
        {
            error.InnerError = CreateError(ex.InnerException);
        }
        
        if (ex is IStatusCodeException statusCodeException)
        {
            response.StatusCode = statusCodeException.StatusCode;
            response.Code = ParseCode(statusCodeException.StatusCode);
        }

        response.Error = error;
        return response;
    }

    private static Error CreateError(Exception ex)
    {
        var details = ex.Data.ToList()
        return new Error
        {
            Details = ex.Data,
            Message = ex.Message
        };
    }

    private static string ParseCode(int statusCode)
    {
        return Enum.Parse<HttpStatusCode>(statusCode.ToString(), true).ToString();
    }
}