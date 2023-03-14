namespace CleanServices.API.Contracts.Error.Responses;

public struct ErrorResponse
{
    public string Code { get; set; }
    public int StatusCode { get; set; }
    public Error Error { get; set; }
}