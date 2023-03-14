using System.Collections;

namespace CleanServices.API.Contracts.Error.Responses;

public class Error
{
    public string Message { get; set; }
    public IDictionary? Data { get; set; }
    public Error? InnerError { get; set; }
}