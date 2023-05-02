using Prism.Regions;

namespace Learn.TemplateStudio.Helpers;

public static class IRegionNavigationServiceExtensions
{
    public static bool CanNavigate(this IRegionNavigationService navigationService, string target)
    {
        if (string.IsNullOrEmpty(target))
        {
            return false;
        }

        if (navigationService.Journal.CurrentEntry == null)
        {
            return true;
        }

        return target != navigationService.Journal.CurrentEntry.Uri.ToString();
    }
}
