using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Imlight.Hmi.Module.Authorization.Services;

public class CustomAuthorizationService
{
    private readonly AuthorizationOptions _authOptions;
    private readonly IAuthorizationHandlerContextFactory _contextFactory;
    private readonly IAuthorizationEvaluator _authEvaluator;
    private readonly IAuthorizationHandler _authHandler;
    private readonly UserSession _userSession;

    public CustomAuthorizationService(
        AuthorizationOptions authOptions,
        IAuthorizationHandlerContextFactory contextFactory,
        IAuthorizationEvaluator authEvaluator,
        IAuthorizationHandler authHandler,
        UserSession userSession)
    {
        _authOptions = authOptions;
        _contextFactory = contextFactory;
        _authEvaluator = authEvaluator;
        _authHandler = authHandler;
        _userSession = userSession;
    }

    public AuthorizationResult Authorize(string policy)
    {
        return AuthorizeAsync(policy).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public async Task<AuthorizationResult> AuthorizeAsync(string policy)
    {
        var currentPolicy = _authOptions.GetPolicy(policy);

        if (currentPolicy is null)
        {
            throw new ArgumentException($"No registered policy named '{policy}' was found");
        }

        var context = _contextFactory.CreateContext(
            currentPolicy.Requirements,
            new ClaimsPrincipal(_userSession.User),
            resource: null
        );

        await _authHandler.HandleAsync(context);
        
        return _authEvaluator.Evaluate(context);
    }
}