namespace CleanServices.API.Contracts.Client.Requests.Create;

public struct ClientCreateRequest
{
    public ClientInfoCreateRequest Info { get; set; }
    public ClientCredentialsCreateRequest Credentials { get; set; }
    public ClientProfileCreateRequest Profile { get; set; }
}