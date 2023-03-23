namespace CleanServices.API.Contracts.Client.Requests.Create;

public struct ClientCredentialsCreateRequest
{
    public string Password { get; set; }
    public string Login { get; set; }
}