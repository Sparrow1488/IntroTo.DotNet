using Microsoft.AspNetCore.Authorization;

namespace Learn.TemplateStudio.Services;

public class CustomAuthorizationService
{
    private readonly IAuthorizationHandlerContextFactory _contextFactory;
    private readonly IAuthorizationEvaluator _authEvaluator;
    private readonly IAuthorizationHandler _authHandler;

    public CustomAuthorizationService(
        IAuthorizationHandlerContextFactory contextFactory,
        IAuthorizationEvaluator authEvaluator,
        IAuthorizationHandler authHandler)
    {
        _contextFactory = contextFactory;
        _authEvaluator = authEvaluator;
        _authHandler = authHandler;
    }

    public Task AuthorizeUserAsync()
    {
        return Task.CompletedTask;
    }
}