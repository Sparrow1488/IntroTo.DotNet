using System.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace Learn.MultipleFrameworks.Events.Models;

public class DialogCloseContext
{
    public Window Host { get; set; } = null!;
    public BaseMetroDialog MetroDialog { get; set; } = null!;
}