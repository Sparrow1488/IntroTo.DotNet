using System.Collections.Generic;

namespace Learn.MultipleFrameworks.Models;

public class Keyboard
{
    public LayoutType LayoutType { get; set; }
    public List<KeyButton> KeyButtons { get; set; }
}