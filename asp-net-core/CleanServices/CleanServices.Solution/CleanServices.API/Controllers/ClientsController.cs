using AutoMapper;
using CleanServices.API.Contracts.Client.Requests.Create;
using CleanServices.API.Contracts.Client.Requests.Update;
using CleanServices.API.Contracts.Client.Responses;
using CleanServices.API.Infrastructure.Exceptions.StatusCode;
using CleanServices.API.Infrastructure.Services.Clients;
using CleanServices.API.Infrastructure.Services.Results;
using Microsoft.AspNetCore.Mvc;

namespace CleanServices.API.Controllers;

public class ClientsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IClientsService _clientsService;

    public ClientsController(IMapper mapper, IClientsService clientsService)
    {
        _mapper = mapper;
        _clientsService = clientsService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var client = await _clientsService.GetByIdAsync(id);
        if (client is null) 
            return Exception(new NotFoundException("Client not found", id, "Client"));
        
        var response = _mapper.Map<ClientResponse>(client);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(ClientCredentialsCreateRequest request)
    {
        var result = await _clientsService.CreateAsync(request.Login, request.Password, null);
        if (result.Status != ResultStatus.Success) 
            return Exception(result.Exception!);
        
        var response = _mapper.Map<ClientResponse>(result.Entity);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateClient(int id, ClientUpdateRequest request)
    {
        var client = await _clientsService.GetByIdAsync(id);
        if (client is null)
            return Exception(new NotFoundException("Client not found", id, "Client"));

        _mapper.Map(request, client);

        var updateResult = await _clientsService.UpdateAsync(client);
        if (updateResult.Status != ResultStatus.Success) 
            return Exception(updateResult.Exception!);
        
        var response = _mapper.Map<ClientResponse>(updateResult.Entity);
        return Ok(response);
    }
}