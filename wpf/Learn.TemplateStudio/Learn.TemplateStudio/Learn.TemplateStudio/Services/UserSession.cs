using System.Security.Claims;

namespace Learn.TemplateStudio.Services;

public class UserSession
{
    public ClaimsIdentity User { get; private set; }

    public void Authenticate(ClaimsIdentity user)
    {
        User = user;
    }
}