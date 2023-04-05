using System.Collections.Generic;

namespace Learn.MultipleFrameworks.Models;

public class Keyboard
{
    public KeyboardLayout Layout { get; set; }
    public List<KeyButton> KeyButtons { get; set; }
}