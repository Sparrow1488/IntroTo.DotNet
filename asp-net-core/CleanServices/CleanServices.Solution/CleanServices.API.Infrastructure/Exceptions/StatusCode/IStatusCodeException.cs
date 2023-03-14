namespace CleanServices.API.Infrastructure.Exceptions.StatusCode;

public interface IStatusCodeException
{
    int StatusCode { get; }
}