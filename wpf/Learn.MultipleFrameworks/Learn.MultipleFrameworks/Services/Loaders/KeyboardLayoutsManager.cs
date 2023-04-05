using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Learn.MultipleFrameworks.Models;
using Newtonsoft.Json;

namespace Learn.MultipleFrameworks.Services.Loaders;

public class KeyboardLayoutsManager
{
    private const string BasePath = "./Resources/Keyboard";
    private const string SaveExtension = ".json";

    public KeyboardLayoutsManager()
    {
        Directory.CreateDirectory(BasePath);
    }
    
    public List<KeyButton> LoadLayout(KeyboardLayout layout)
    {
        var loadPath = Path.Combine(BasePath, layout + SaveExtension);
        if (!File.Exists(loadPath)) 
            return new List<KeyButton>();

        var jsonKeyboard = File.ReadAllText(loadPath);
        var keyboard = JsonConvert.DeserializeObject<Keyboard>(jsonKeyboard);
        
        return keyboard?.KeyButtons ?? new List<KeyButton>();
    }
    
    public Task SaveLayoutAsync(List<KeyButton> keyButtons, KeyboardLayout layout)
    {
        var savePath = Path.Combine(BasePath, layout + SaveExtension);
        var keyboard = new Keyboard
        {
            KeyButtons = keyButtons,
            Layout = layout
        };

        var jsonKeyboard = JsonConvert.SerializeObject(keyboard, Formatting.Indented);
        return File.WriteAllTextAsync(savePath, jsonKeyboard);
    }
}