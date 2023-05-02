using Learn.TemplateStudio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Prism.Ioc;

namespace Learn.TemplateStudio.Extensions;

public static class AuthorizationExtensions
{
    public static void AddAuthorization(this IContainerRegistry container, Action<AuthorizationOptions> configure)
    {
        var options = new AuthorizationOptions();
        configure.Invoke(options);
        
        container.RegisterInstance(options);
        container.AddAuthorizationServices();
    }

    private static void AddAuthorizationServices(this IContainerRegistry container)
    {
        container.Register<IAuthorizationHandlerContextFactory, DefaultAuthorizationHandlerContextFactory>();
        container.Register<IAuthorizationHandler, PassThroughAuthorizationHandler>();
        container.Register<IAuthorizationEvaluator, DefaultAuthorizationEvaluator>();
        container.RegisterSingleton<UserSession>();
    }
}