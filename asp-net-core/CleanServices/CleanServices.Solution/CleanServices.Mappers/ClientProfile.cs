using AutoMapper;
using CleanServices.API.Contracts.Client.Requests;
using CleanServices.API.Contracts.Client.Responses;
using CleanServices.Models.Clients;
using CleanServices.Models.Clients.Credentials;
using CleanServices.Models.Clients.Info;

namespace CleanServices.Mappers;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        #region Requests

        CreateMap<ClientCreateRequest, Client>()
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<ClientInfoCreateRequest, ClientInfo>()
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<ClientProfileCreateRequest, Models.Clients.Profile.ClientProfile>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        
        CreateMap<ClientCredentialsCreateRequest, ClientCredentials>()
            .ForMember(x => x.Id, opt => opt.Ignore());

        #endregion

        #region Responses

        CreateMap<Client, ClientResponse>();

        CreateMap<Models.Clients.Profile.ClientProfile, ClientProfileResponse>();

        CreateMap<ClientInfo, ClientInfoResponse>();

        #endregion
    }
}