using System.Security.Claims;

namespace Imlight.Hmi.Module.Authorization;

public class UserSession
{
    public ClaimsIdentity User { get; private set; }

    public void Authenticate(ClaimsIdentity user)
    {
        User = user;
    }
}