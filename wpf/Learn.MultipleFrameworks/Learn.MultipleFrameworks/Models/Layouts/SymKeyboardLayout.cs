using System.Collections.Generic;

namespace Learn.MultipleFrameworks.Models.Layouts;

public class SymKeyboardLayout : KeyboardLayout
{
    public SymKeyboardLayout(IEnumerable<KeyButton> keys) : base(keys) { }

    public override LayoutState State => LayoutState.Sym;
    public override LayoutType Type => LayoutType.Symbols;
}