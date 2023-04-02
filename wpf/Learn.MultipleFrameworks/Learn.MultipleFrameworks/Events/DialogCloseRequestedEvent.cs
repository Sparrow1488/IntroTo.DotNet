using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;

namespace Learn.MultipleFrameworks.Events;

public class DialogCloseRequestedEvent : PubSubEvent<DialogCloseContext>
{
    
}

public class DialogCloseContext
{
    public Window Host { get; set; }
    public BaseMetroDialog MetroDialog { get; set; }
}