namespace Learn.MultipleFrameworks.Events.Models;

public class KeyboardInput
{
    public KeyboardInput(string value, bool openedInDialog)
    {
        Value = value;
        OpenedInDialog = openedInDialog;
    }
    
    public string Value { get; }
    public bool OpenedInDialog { get; }
}