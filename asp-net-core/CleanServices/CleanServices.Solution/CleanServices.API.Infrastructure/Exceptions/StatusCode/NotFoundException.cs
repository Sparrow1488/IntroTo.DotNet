using Microsoft.AspNetCore.Http;

namespace CleanServices.API.Infrastructure.Exceptions.StatusCode;

public sealed class NotFoundException : ApiException, IStatusCodeException
{
    public NotFoundException(
        string message, 
        object? entityId = null, 
        string entityType = "undefined_entity_type"
    ) : base(message)
    {
        Data.Add("entity_id", entityId ?? "undefined_entity_id");
        Data.Add("entity_type", entityType);
    }

    public int StatusCode => StatusCodes.Status404NotFound;
}