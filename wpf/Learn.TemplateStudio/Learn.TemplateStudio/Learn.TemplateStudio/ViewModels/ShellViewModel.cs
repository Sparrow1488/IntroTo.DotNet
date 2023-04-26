using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using Learn.TemplateStudio.Constants;
using Learn.TemplateStudio.Contracts.Services;
using Learn.TemplateStudio.Core.Contracts.Services;
using Learn.TemplateStudio.Core.Helpers;
using Learn.TemplateStudio.Helpers;
using Learn.TemplateStudio.Properties;

using MahApps.Metro.Controls;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Learn.TemplateStudio.ViewModels;

public class ShellViewModel : BindableBase, IDisposable
{
    private readonly IRegionManager _regionManager;
    private readonly IIdentityService _identityService;
    private readonly IUserDataService _userDataService;
    private IRegionNavigationService _navigationService;
    private HamburgerMenuItem _selectedMenuItem;
    private HamburgerMenuItem _selectedOptionsMenuItem;
    private bool _isBusy;
    private bool _isLoggedIn;
    private bool _isAuthorized;
    private DelegateCommand _goBackCommand;
    private ICommand _loadedCommand;
    private ICommand _unloadedCommand;
    private ICommand _menuItemInvokedCommand;
    private ICommand _optionsMenuItemInvokedCommand;

    public HamburgerMenuItem SelectedMenuItem
    {
        get { return _selectedMenuItem; }
        set { SetProperty(ref _selectedMenuItem, value); }
    }

    public HamburgerMenuItem SelectedOptionsMenuItem
    {
        get { return _selectedOptionsMenuItem; }
        set { SetProperty(ref _selectedOptionsMenuItem, value); }
    }

    // TODO: Change the icons and titles for all HamburgerMenuItems here.
    public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new()
    {
        new HamburgerMenuGlyphItem { Label = Resources.ShellMainPage, Glyph = "\uE8A5", Tag = PageKeys.Main },
        new HamburgerMenuGlyphItem { Label = Resources.ShellHomePage, Glyph = "\uE8A5", Tag = PageKeys.Home },
        new HamburgerMenuGlyphItem { Label = Resources.ShellSecurePage, Glyph = "\uE8A5", Tag = PageKeys.Secure },
    };

    public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new()
    {
        new HamburgerMenuGlyphItem { Label = Resources.ShellSettingsPage, Glyph = "\uE713", Tag = PageKeys.Settings }
    };

    public Func<HamburgerMenuItem, bool> IsPageRestricted { get; } = (menuItem) =>
    {
        var app = Application.Current as App;
        var page = app.GetPageType(menuItem.Tag.ToString());
        return Attribute.IsDefined(page.GetType(), typeof(Restricted));
    };

    public bool IsBusy
    {
        get { return _isBusy; }
        set { SetProperty(ref _isBusy, value); }
    }

    public bool IsLoggedIn
    {
        get { return _isLoggedIn; }
        set { SetProperty(ref _isLoggedIn, value); }
    }

    public bool IsAuthorized
    {
        get { return _isAuthorized; }
        set { SetProperty(ref _isAuthorized, value); }
    }

    public DelegateCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(OnGoBack, CanGoBack));

    public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(OnLoaded));

    public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new DelegateCommand(OnUnloaded));

    public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new DelegateCommand(OnMenuItemInvoked));

    public ICommand OptionsMenuItemInvokedCommand => _optionsMenuItemInvokedCommand ?? (_optionsMenuItemInvokedCommand = new DelegateCommand(OnOptionsMenuItemInvoked));

    public ShellViewModel(IRegionManager regionManager, IIdentityService identityService, IUserDataService userDataService)
    {
        _regionManager = regionManager;
        _identityService = identityService;
        _userDataService = userDataService;
        _identityService.LoggedIn += OnLoggedIn;
        _identityService.LoggedOut += OnLoggedOut;
        _userDataService.UserDataUpdated += OnUserDataUpdated;
    }

    public void Dispose()
    {
        _userDataService.UserDataUpdated -= OnUserDataUpdated;
    }

    private void OnUserDataUpdated(object sender, UserViewModel user)
    {
        var userMenuItem = OptionMenuItems.OfType<HamburgerMenuImageItem>().FirstOrDefault();
        if (userMenuItem != null)
        {
            userMenuItem.Label = user.Name;
            userMenuItem.Thumbnail = user.Photo;
        }
    }

    private void OnLoggedIn(object sender, EventArgs e)
    {
        IsLoggedIn = true;
        IsAuthorized = IsLoggedIn && _identityService.IsAuthorized();
        IsBusy = false;
    }

    private void OnLoggedOut(object sender, EventArgs e)
    {
        IsLoggedIn = false;
        IsAuthorized = false;
        RemoveUserInformation();
        _navigationService.Journal.Clear();
    }

    private void RemoveUserInformation()
    {
        var userMenuItem = OptionMenuItems.OfType<HamburgerMenuImageItem>().FirstOrDefault();
        if (userMenuItem != null)
        {
            userMenuItem.Thumbnail = ImageHelper.ImageFromAssetsFile("DefaultIcon.png");
            userMenuItem.Label = Resources.Shell_LogIn;
        }
    }

    private void OnLoaded()
    {
        _navigationService = _regionManager.Regions[Regions.Main].NavigationService;
        _navigationService.Navigated += OnNavigated;
        SelectedMenuItem = MenuItems.First();
        IsLoggedIn = _identityService.IsLoggedIn();
        IsAuthorized = IsLoggedIn && _identityService.IsAuthorized();
        var userMenuItem = new HamburgerMenuImageItem()
        {
            Command = new DelegateCommand(OnUserItemSelected, () => !IsBusy)
        };
        if (IsAuthorized)
        {
            var user = _userDataService.GetUser();
            userMenuItem.Thumbnail = user.Photo;
            userMenuItem.Label = user.Name;
        }
        else
        {
            userMenuItem.Thumbnail = ImageHelper.ImageFromAssetsFile("DefaultIcon.png");
            userMenuItem.Label = Resources.Shell_LogIn;
        }

        OptionMenuItems.Insert(0, userMenuItem);
    }

    private async void OnUserItemSelected()
    {
        if (!IsLoggedIn)
        {
            IsBusy = true;
            // var loginResult = await _identityService.LoginAsync();
            var loginResult = LoginResultType.Success;
            if (loginResult != LoginResultType.Success)
            {
                await AuthenticationHelper.ShowLoginErrorAsync(loginResult);
                IsBusy = false;
            }
        }

        RequestNavigate(PageKeys.Settings);
    }

    private void OnUnloaded()
    {
        _navigationService.Navigated -= OnNavigated;
        _regionManager.Regions.Remove(Regions.Main);
    }

    private bool CanGoBack()
        => _navigationService != null && _navigationService.Journal.CanGoBack;

    private void OnGoBack()
        => _navigationService.Journal.GoBack();

    private void OnMenuItemInvoked()
        => RequestNavigate(SelectedMenuItem.Tag?.ToString());

    private void OnOptionsMenuItemInvoked()
        => RequestNavigate(SelectedOptionsMenuItem.Tag?.ToString());

    private void RequestNavigate(string target)
    {
        if (_navigationService.CanNavigate(target))
        {
            _navigationService.RequestNavigate(target);
        }
    }

    private void OnNavigated(object sender, RegionNavigationEventArgs e)
    {
        var item = MenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => e.Uri.ToString() == i.Tag?.ToString());
        if (item != null)
        {
            SelectedMenuItem = item;
        }
        else
        {
            SelectedOptionsMenuItem = OptionMenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => e.Uri.ToString() == i.Tag?.ToString());
        }

        GoBackCommand.RaiseCanExecuteChanged();
    }
}
