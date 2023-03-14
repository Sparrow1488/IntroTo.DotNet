namespace CleanServices.API.Contracts.Client.Requests;

public struct ClientCredentialsCreateRequest
{
    public string Password { get; set; }
    public string Login { get; set; }
}