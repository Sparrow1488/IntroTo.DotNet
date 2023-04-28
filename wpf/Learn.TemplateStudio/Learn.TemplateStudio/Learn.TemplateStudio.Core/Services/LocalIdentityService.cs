using System.Security.Claims;
using Learn.TemplateStudio.Core.Contracts.Services;

namespace Learn.TemplateStudio.Core.Services;

public class LocalIdentityService : ILocalIdentityService
{
    public event Action<bool> OnAuthorized;
    public ClaimsIdentity User { get; private set; }    
    
    public ClaimsIdentity AuthorizeUser(string password)
    {
        if (password == "1234")
        {
            var claims = new List<Claim>
            {
                new("app.claim", "secret_page_view"),
            };
            
            User = new ClaimsIdentity(claims);
        }

        OnAuthorized?.Invoke(IsAuthorized());
        return User;
    }

    public bool IsAuthorized() => User is not null;
}