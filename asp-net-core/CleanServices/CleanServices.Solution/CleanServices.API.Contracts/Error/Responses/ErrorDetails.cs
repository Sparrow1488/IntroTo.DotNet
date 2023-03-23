namespace CleanServices.API.Contracts.Error.Responses;

public class ErrorDetails
{
    public string Code { get; set; }
    public string Target { get; set; }
    public string Message { get; set; }
}