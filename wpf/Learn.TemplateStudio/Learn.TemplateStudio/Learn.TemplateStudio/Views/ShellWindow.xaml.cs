using Learn.TemplateStudio.Constants;

using MahApps.Metro.Controls;

using Prism.Regions;

namespace Learn.TemplateStudio.Views;

public partial class ShellWindow : MetroWindow
{
    public ShellWindow(IRegionManager regionManager)
    {
        InitializeComponent();
        RegionManager.SetRegionName(HamburgerMenuContentControl, Regions.Main);
        RegionManager.SetRegionManager(HamburgerMenuContentControl, regionManager);
    }
}
