namespace CleanServices.API.Contracts.Client.Requests.Update;

public struct ClientUpdateRequest
{
    public ClientInfoUpdateRequest? Info { get; set; }
    public ClientProfileUpdateRequest? Profile { get; set; }
}