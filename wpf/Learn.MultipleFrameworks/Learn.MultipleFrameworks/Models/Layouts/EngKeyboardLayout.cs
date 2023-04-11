using System.Collections.Generic;

namespace Learn.MultipleFrameworks.Models.Layouts;

public class EngKeyboardLayout : KeyboardLayout
{
    public EngKeyboardLayout(IEnumerable<KeyButton> keys) : base(keys) { }

    public override LayoutState State => LayoutState.Abs;
    public override LayoutType Type => LayoutType.EN;
}