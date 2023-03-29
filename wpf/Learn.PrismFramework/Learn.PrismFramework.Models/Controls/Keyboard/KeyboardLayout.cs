using System.Collections.ObjectModel;

namespace Learn.PrismFramework.Models.Controls.Keyboard;

public class KeyboardLayout
{
    public ObservableCollection<KeyButton> Buttons { get; set; }
}