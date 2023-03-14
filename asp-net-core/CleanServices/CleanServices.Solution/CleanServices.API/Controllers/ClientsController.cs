using AutoMapper;
using CleanServices.API.Contracts.Client.Requests;
using CleanServices.API.Contracts.Client.Responses;
using CleanServices.API.Infrastructure.Exceptions.StatusCode;
using CleanServices.API.Infrastructure.Extensions;
using CleanServices.API.Infrastructure.Services.Clients;
using CleanServices.API.Infrastructure.Services.Results;
using CleanServices.Models.Clients;
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

        if (client is not null)
        {
            var response = _mapper.Map<ClientResponse>(client);
            return Ok(response);
        }

        var ex = new NotFoundException("Client not found", id, "Client");
        var errorResponse = ex.ToErrorResponse();
        return StatusCode(errorResponse.StatusCode, errorResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(ClientCreateRequest request)
    {
        var credentials = request.Credentials;
        var client = _mapper.Map<Client>(request);

        var result = await _clientsService.CreateAsync(client, credentials.Login, credentials.Password, null);
        if (result.Status == ResultStatus.Success)
        {
            var response = _mapper.Map<ClientResponse>(result.Entity);
            return Ok(response);
        }

        var errorResponse = result.Exception!.ToErrorResponse();
        return StatusCode(errorResponse.StatusCode, errorResponse);
    }
}