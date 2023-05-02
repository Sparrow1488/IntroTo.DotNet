using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Windows;
using System.Windows.Threading;
using Imlight.Hmi.Module.Authorization;
using Imlight.Hmi.Module.Authorization.Extensions;
using Learn.TemplateStudio.Constants;
using Learn.TemplateStudio.Contracts.Services;
using Learn.TemplateStudio.Core.Contracts.Services;
using Learn.TemplateStudio.Core.Services;
using Learn.TemplateStudio.Models;
using Learn.TemplateStudio.Services;
using Learn.TemplateStudio.ViewModels;
using Learn.TemplateStudio.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Unity;

using Unity;

namespace Learn.TemplateStudio;

public partial class App
{
    public const string Scheme = "TemplateStudio";

    private string[] _startUpArgs;

    public object GetPageType(string pageKey)
        => Container.Resolve<object>(pageKey);

    protected override Window CreateShell()
        => Container.Resolve<ShellWindow>();

    protected override async void OnInitialized()
    {
        var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
        persistAndRestoreService.RestoreData();

        var themeSelectorService = Container.Resolve<IThemeSelectorService>();
        themeSelectorService.InitializeTheme();

        var userDataService = Container.Resolve<IUserDataService>();
        userDataService.Initialize();

        var config = Container.Resolve<AppConfig>();
        var identityService = Container.Resolve<IIdentityService>();
        identityService.InitializeWithAadAndPersonalMsAccounts(config.IdentityClientId, "http://localhost");

        await identityService.AcquireTokenSilentAsync();
        
        var localIdentityService = Container.Resolve<ILocalIdentityService>();
        if (!localIdentityService.IsAuthorized())
        {
            localIdentityService.AuthorizeUser("1234");
        }
        
        base.OnInitialized();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _startUpArgs = e.Args;
        base.OnStartup(e);
    }

    protected override void RegisterTypes(IContainerRegistry services)
    {
        // Authorization, регистрируется перед добавлением ViewModels
        services.Register<DefaultAuthorizationService>();
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("User", builder =>
            {
                builder.AddAuthenticationSchemes(Scheme);
                builder.RequireClaim(
                    "role", 
                    "role.user", "role.admin"
                );
            });
            
            options.AddPolicy("Admin", builder =>
            {
                builder.AddAuthenticationSchemes(Scheme);
                builder.RequireClaim("role", "role.admin");
            });
            
            options.AddPolicy("SeeSecretPage", builder =>
            {
                builder.AddAuthenticationSchemes(Scheme);
                builder.RequireAuthenticatedUser();
                builder.RequireClaim("permission", "permission.see_secret_page");
            });
        });

        // TODO: session.Authenticate(identity) вызывается в LoginViewModel
        var session = Container.Resolve<UserSession>();
        session.Authenticate(AuthenticateUser());
        
        // Core Services
        services.Register<IMicrosoftGraphService, MicrosoftGraphService>();
        services.GetContainer().RegisterFactory<IHttpClientFactory>(container => GetHttpClientFactory());
        services.Register<IIdentityCacheService, IdentityCacheService>();
        services.RegisterSingleton<IIdentityService, IdentityService>();
        services.RegisterSingleton<ILocalIdentityService, LocalIdentityService>();
        services.Register<IFileService, FileService>();

        // App Services
        services.RegisterSingleton<IUserDataService, UserDataService>();
        services.Register<IApplicationInfoService, ApplicationInfoService>();
        services.Register<ISystemService, SystemService>();
        services.Register<IPersistAndRestoreService, PersistAndRestoreService>();
        services.Register<IThemeSelectorService, ThemeSelectorService>();

        // Views
        services.RegisterForNavigation<SettingsPage, SettingsViewModel>(PageKeys.Settings);
        services.RegisterForNavigation<SecurePage, SecureViewModel>(PageKeys.Secure);
        services.RegisterForNavigation<HomePage, HomeViewModel>(PageKeys.Home);
        services.RegisterForNavigation<MainPage, MainViewModel>(PageKeys.Main);
        services.RegisterForNavigation<ShellWindow, ShellViewModel>();
        
        // Configuration
        var configuration = BuildConfiguration();
        var appConfig = configuration
            .GetSection(nameof(AppConfig))
            .Get<AppConfig>();

        // Register configurations to IoC
        services.RegisterInstance(configuration);
        services.RegisterInstance(appConfig);
    }

    private static ClaimsIdentity AuthenticateUser()
    {
        return new ClaimsIdentity(new Claim[]
        {
            new("role", "role.admin"),
            new("permission", "permission.see_secret_page")
        }, Scheme);
    }

    private IHttpClientFactory GetHttpClientFactory()
    {
        var services = new ServiceCollection();
        services.AddHttpClient("msgraph", client =>
        {
            client.BaseAddress = new Uri("https://graph.microsoft.com/v1.0/");
        });

        return services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
    }

    private IConfiguration BuildConfiguration()
    {
        var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
        return new ConfigurationBuilder()
            .SetBasePath(appLocation)
            .AddJsonFile("appsettings.json")
            .AddCommandLine(_startUpArgs)
            .Build();
    }

    private void OnExit(object sender, ExitEventArgs e)
    {
        var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
        persistAndRestoreService.PersistData();
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // TODO: Please log and handle the exception as appropriate to your scenario
        // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
    }
}
