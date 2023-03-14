namespace CleanServices.API.Contracts.Client.Responses;

public struct ClientResponse
{
    public int Id { get; set; }
    public ClientInfoResponse Info { get; set; }
    public ClientProfileResponse Profile { get; set; }
}