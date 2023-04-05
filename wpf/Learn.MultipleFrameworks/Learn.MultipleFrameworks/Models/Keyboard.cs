using System.Collections.Generic;
using Learn.MultipleFrameworks.ViewModels;

namespace Learn.MultipleFrameworks.Models;

public class Keyboard
{
    public KeyboardLayout Layout { get; set; }
    public List<KeyButton> KeyButtons { get; set; }
}