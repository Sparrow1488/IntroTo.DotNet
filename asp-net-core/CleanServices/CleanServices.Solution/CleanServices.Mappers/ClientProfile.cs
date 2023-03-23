using AutoMapper;
using CleanServices.API.Contracts.Client.Requests.Create;
using CleanServices.API.Contracts.Client.Requests.Update;
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

        #region Create

        CreateMap<ClientCreateRequest, Client>()
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<ClientInfoCreateRequest, ClientInfo>()
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<ClientProfileCreateRequest, Models.Clients.Profile.ClientProfile>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        
        CreateMap<ClientCredentialsCreateRequest, ClientCredentials>()
            .ForMember(x => x.Id, opt => opt.Ignore());

        #endregion

        #region Update

        CreateMap<ClientUpdateRequest, Client>()
            .ForMember(x => x.Credentials, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, _, srcMember) => srcMember != null));

        CreateMap<ClientInfoUpdateRequest, ClientInfo>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, _, srcMember) => srcMember != null));

        CreateMap<ClientProfileUpdateRequest, Models.Clients.Profile.ClientProfile>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, _, srcMember) => srcMember != null));
        
        #endregion
        
        #endregion

        #region Responses

        CreateMap<Client, ClientResponse>();

        CreateMap<Models.Clients.Profile.ClientProfile, ClientProfileResponse>();

        CreateMap<ClientInfo, ClientInfoResponse>();

        #endregion
    }
}