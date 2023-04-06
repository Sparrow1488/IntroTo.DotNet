using System.Collections.Generic;

namespace Learn.MultipleFrameworks.Models.Layouts;

public class RusKeyboardLayout : KeyboardLayout
{
    public RusKeyboardLayout(IEnumerable<KeyButton> keys) : base(keys) { }

    public override LayoutState State => LayoutState.Abs;
    public override LayoutType Type => LayoutType.RU;
}