using System.Windows;

using Learn.TemplateStudio.Core.Helpers;
using Learn.TemplateStudio.Properties;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Learn.TemplateStudio.Helpers;

internal static class AuthenticationHelper
{
    internal static async Task ShowLoginErrorAsync(LoginResultType loginResult)
    {
        var metroWindow = Application.Current.MainWindow as MetroWindow;
        switch (loginResult)
        {
            case LoginResultType.NoNetworkAvailable:
                await metroWindow.ShowMessageAsync(Resources.DialogNoNetworkAvailableContent, Resources.DialogAuthenticationTitle);
                break;
            case LoginResultType.UnknownError:
                await metroWindow.ShowMessageAsync(Resources.DialogAuthenticationTitle, Resources.DialogStatusUnknownErrorContent);
                break;
        }
    }
}
