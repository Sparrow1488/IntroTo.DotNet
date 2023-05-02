using Microsoft.AspNetCore.Authorization;
using Prism.Ioc;

namespace Learn.TemplateStudio.Extensions;

public static class AuthorizationExtensions
{
    public static void AddAuthorization(this IContainerRegistry container, Action<AuthorizationOptions> configure)
    {
        var options = new AuthorizationOptions();
        configure.Invoke(options);
        
        container.RegisterInstance(options);
    }
}