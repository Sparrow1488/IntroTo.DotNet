using System.Windows.Input;

using Learn.TemplateStudio.Contracts.Services;
using Learn.TemplateStudio.Core.Contracts.Services;
using Learn.TemplateStudio.Core.Helpers;
using Learn.TemplateStudio.Helpers;
using Learn.TemplateStudio.Models;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Learn.TemplateStudio.ViewModels;

// TODO: Change the URL for your privacy policy in the appsettings.json file, currently set to https://YourPrivacyUrlGoesHere
public class SettingsViewModel : BindableBase, INavigationAware
{
    private readonly AppConfig _appConfig;
    private readonly IUserDataService _userDataService;
    private readonly IIdentityService _identityService;
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly ISystemService _systemService;
    private readonly IApplicationInfoService _applicationInfoService;
    private AppTheme _theme;
    private string _versionDescription;
    private bool _isBusy;
    private bool _isLoggedIn;
    private UserViewModel _user;
    private ICommand _setThemeCommand;
    private ICommand _privacyStatementCommand;
    private DelegateCommand _logInCommand;
    private DelegateCommand _logOutCommand;

    public AppTheme Theme
    {
        get { return _theme; }
        set { SetProperty(ref _theme, value); }
    }

    public string VersionDescription
    {
        get { return _versionDescription; }
        set { SetProperty(ref _versionDescription, value); }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            SetProperty(ref _isBusy, value);
            LogInCommand.RaiseCanExecuteChanged();
            LogOutCommand.RaiseCanExecuteChanged();
        }
    }

    public bool IsLoggedIn
    {
        get { return _isLoggedIn; }
        set { SetProperty(ref _isLoggedIn, value); }
    }

    public UserViewModel User
    {
        get { return _user; }
        set { SetProperty(ref _user, value); }
    }

    public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new DelegateCommand<string>(OnSetTheme));

    public ICommand PrivacyStatementCommand => _privacyStatementCommand ?? (_privacyStatementCommand = new DelegateCommand(OnPrivacyStatement));

    public DelegateCommand LogInCommand => _logInCommand ?? (_logInCommand = new DelegateCommand(OnLogIn, () => !IsBusy));

    public DelegateCommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new DelegateCommand(OnLogOut, () => !IsBusy));

    public SettingsViewModel(AppConfig appConfig, IThemeSelectorService themeSelectorService, ISystemService systemService, IApplicationInfoService applicationInfoService, IUserDataService userDataService, IIdentityService identityService)
    {
        _appConfig = appConfig;
        _themeSelectorService = themeSelectorService;
        _systemService = systemService;
        _applicationInfoService = applicationInfoService;
        _userDataService = userDataService;
        _identityService = identityService;
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
        Theme = _themeSelectorService.GetCurrentTheme();
        _identityService.LoggedIn += OnLoggedIn;
        _identityService.LoggedOut += OnLoggedOut;
        IsLoggedIn = _identityService.IsLoggedIn();
        _userDataService.UserDataUpdated += OnUserDataUpdated;
        User = _userDataService.GetUser();
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        UnregisterEvents();
    }

    private void UnregisterEvents()
    {
        _identityService.LoggedIn -= OnLoggedIn;
        _identityService.LoggedOut -= OnLoggedOut;
        _userDataService.UserDataUpdated -= OnUserDataUpdated;
    }

    private void OnSetTheme(string themeName)
    {
        var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
        _themeSelectorService.SetTheme(theme);
    }

    private void OnPrivacyStatement()
        => _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);

    public bool IsNavigationTarget(NavigationContext navigationContext)
        => true;

    private async void OnLogIn()
    {
        IsBusy = true;
        var loginResult = await _identityService.LoginAsync();
        if (loginResult != LoginResultType.Success)
        {
            await AuthenticationHelper.ShowLoginErrorAsync(loginResult);
            IsBusy = false;
        }
    }

    private async void OnLogOut()
    {
        await _identityService.LogoutAsync();
    }

    private void OnUserDataUpdated(object sender, UserViewModel userData)
    {
        User = userData;
    }

    private void OnLoggedIn(object sender, EventArgs e)
    {
        IsLoggedIn = true;
        IsBusy = false;
    }

    private void OnLoggedOut(object sender, EventArgs e)
    {
        User = null;
        IsLoggedIn = false;
        IsBusy = false;
    }
}
