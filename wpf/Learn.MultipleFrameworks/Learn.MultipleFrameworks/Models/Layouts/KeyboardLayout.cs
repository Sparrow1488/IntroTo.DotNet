using System.Collections.Generic;
using System.Linq;

namespace Learn.MultipleFrameworks.Models.Layouts;

public abstract class KeyboardLayout
{
    public KeyboardLayout(IEnumerable<KeyButton> keys)
    {
        Keys = keys;
    }
    
    public abstract LayoutState State { get; }
    public abstract LayoutType Type { get; }
    
    public IEnumerable<KeyButton> Keys { get; }

    public IEnumerable<KeyButton> this[int row] => Keys.Where(x => x.Row == row);
}