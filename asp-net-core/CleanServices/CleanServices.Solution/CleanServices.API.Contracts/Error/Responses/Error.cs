namespace CleanServices.API.Contracts.Error.Responses;

public class Error
{
    // public string Code { get; set; }
    public string Message { get; set; }
    public List<ErrorDetails> Details { get; set; }
    public Error? InnerError { get; set; }
}