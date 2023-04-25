using Learn.TemplateStudio.Constants;

using MahApps.Metro.Controls;

using Prism.Regions;

namespace Learn.TemplateStudio.Views;

public partial class ShellWindow : MetroWindow
{
    public ShellWindow(IRegionManager regionManager)
    {
        InitializeComponent();
        RegionManager.SetRegionName(hamburgerMenuContentControl, Regions.Main);
        RegionManager.SetRegionManager(hamburgerMenuContentControl, regionManager);
    }
}
