using System.Security.Claims;

namespace Learn.TemplateStudio.Core.Contracts.Services;

public interface ILocalIdentityService
{
    event Action<bool> OnAuthorized;
    
    ClaimsIdentity User { get; }
    ClaimsIdentity AuthorizeUser(string password);
    bool IsAuthorized();
}