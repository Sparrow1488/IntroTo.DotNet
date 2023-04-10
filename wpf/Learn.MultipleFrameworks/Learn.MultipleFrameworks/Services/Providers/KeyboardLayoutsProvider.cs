using System.Collections.Generic;
using Learn.MultipleFrameworks.Models.Layouts;
using Learn.MultipleFrameworks.Services.Stores;

namespace Learn.MultipleFrameworks.Services.Providers;

public class KeyboardLayoutsProvider
{
    public virtual IEnumerable<KeyboardLayout> GetLayouts()
    {
        return new List<KeyboardLayout>
        {
            KeyboardLayoutsStore.Russian,
            KeyboardLayoutsStore.English,
            KeyboardLayoutsStore.Symbols,
        };
    }
}