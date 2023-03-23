using CleanServices.API.Infrastructure.Exceptions.StatusCode;
using CleanServices.API.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CleanServices.API.Controllers;

[ApiController, Route("v1/[controller]")]
public class ApiController : ControllerBase
{
    protected IActionResult Exception(Exception exception)
    {
        var statusCode = 500;
        if (exception is IStatusCodeException statusCodeException)
            statusCode = statusCodeException.StatusCode;
        
        return StatusCode(statusCode, exception.ToErrorResponse());
    }
}