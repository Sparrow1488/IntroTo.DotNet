using System;
using Learn.MultipleFrameworks.Models.Settings;

namespace Learn.MultipleFrameworks.Services.Keyboards;

public class KeyboardSettingsProvider
{
    public KeyboardSettings KeyboardSettings { get; private set; }
    
    public KeyboardSettings CreateDefault()
    {
        return new KeyboardSettings
        {
            IsPassword = false
        };
    }

    public void ConfigureScopeKeyboardSetting(Action<KeyboardSettings> settings)
    {
        KeyboardSettings = CreateDefault();
        settings.Invoke(KeyboardSettings);
    }
    
    public void ConfigureScopeKeyboardSetting(Func<KeyboardSettings> createSettings)
    {
        KeyboardSettings = createSettings.Invoke();
    }
}