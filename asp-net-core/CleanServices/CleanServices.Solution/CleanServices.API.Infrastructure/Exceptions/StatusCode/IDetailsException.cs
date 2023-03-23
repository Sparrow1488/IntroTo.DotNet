using CleanServices.API.Contracts.Error.Responses;

namespace CleanServices.API.Infrastructure.Exceptions.StatusCode;

public interface IDetailsException
{
    IList<ErrorDetails> Details { get; }
}