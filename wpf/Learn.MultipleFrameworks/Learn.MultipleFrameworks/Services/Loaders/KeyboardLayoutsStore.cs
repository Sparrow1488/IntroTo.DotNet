using System;
using System.Collections.Generic;
using Learn.MultipleFrameworks.Models;
using Learn.MultipleFrameworks.Models.Layouts;

namespace Learn.MultipleFrameworks.Services.Loaders;

public static class KeyboardLayoutsStore
{
    public static List<KeyButton> GetLayout(LayoutType layoutType)
    {
        return layoutType switch
        {
            LayoutType.EN => EnglishLayoutButtons,
            LayoutType.RU => RussianLayoutButtons,
            LayoutType.Symbols => throw new NotImplementedException(),
            LayoutType.FullEN => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(layoutType), layoutType, null)
        };
    }

    public static KeyboardLayout English => new EngKeyboardLayout(EnglishLayoutButtons);
    public static KeyboardLayout Russian => new RusKeyboardLayout(RussianLayoutButtons);

    #region English Layout Buttons

    private static List<KeyButton> EnglishLayoutButtons { get; } =
        new()
        {
            new KeyButton("`", 1), // fix
            new KeyButton("1", 1),
            new KeyButton("2", 1),
            new KeyButton("3", 1),
            new KeyButton("4", 1),
            new KeyButton("5", 1),
            new KeyButton("6", 1),
            new KeyButton("7", 1),
            new KeyButton("8", 1),
            new KeyButton("9", 1),
            new KeyButton("0", 1),
            new KeyButton("%", 1),
            new KeyButton("`", 1), // fix
                    
            new KeyButton("q", 2),
            new KeyButton("w", 2),
            new KeyButton("e", 2),
            new KeyButton("r", 2),
            new KeyButton("t", 2),
            new KeyButton("z", 2),
            new KeyButton("u", 2),
            new KeyButton("i", 2),
            new KeyButton("o", 2),
            new KeyButton("p", 2),
            new KeyButton("/", 2),
            new KeyButton("(", 2),
                    
            new KeyButton("a", 3),
            new KeyButton("s", 3),
            new KeyButton("d", 3),
            new KeyButton("f", 3),
            new KeyButton("g", 3),
            new KeyButton("h", 3),
            new KeyButton("j", 3),
            new KeyButton("k", 3),
            new KeyButton("l", 3),
            new KeyButton("\"", 3),
            new KeyButton("!", 3),
            new KeyButton("'", 3),
                    
            new KeyButton("y", 4),
            new KeyButton("x", 4),
            new KeyButton("c", 4),
            new KeyButton("v", 4),
            new KeyButton("b", 4),
            new KeyButton("n", 4),
            new KeyButton("m", 4),
            new KeyButton("?", 4),
            new KeyButton(":", 4),
            new KeyButton("_", 4),
        };

    #endregion

    #region Russian Layout Buttons

    private static List<KeyButton> RussianLayoutButtons { get; } =
        new()
        {
            new KeyButton("й", 1),
            new KeyButton("ц", 1),
            new KeyButton("у", 1),
            new KeyButton("к", 1),
            new KeyButton("е", 1),
            new KeyButton("н", 1),
            new KeyButton("г", 1),
            new KeyButton("ш", 1),
            new KeyButton("щ", 1),
            new KeyButton("з", 1),
            new KeyButton("х", 1),
            new KeyButton("ъ", 1),

            new KeyButton("ф", 2),
            new KeyButton("ы", 2),
            new KeyButton("в", 2),
            new KeyButton("а", 2),
            new KeyButton("п", 2),
            new KeyButton("р", 2),
            new KeyButton("л", 2),
            new KeyButton("д", 2),
            new KeyButton("ж", 2),
            new KeyButton("э", 2),

            new KeyButton("я", 3),
            new KeyButton("ч", 3),
            new KeyButton("с", 3),
            new KeyButton("м", 3),
            new KeyButton("и", 3),
            new KeyButton("т", 3),
            new KeyButton("ь", 3),
            new KeyButton("б", 3),
            new KeyButton("ю", 3),
            new KeyButton(".", 3),
        };

    #endregion

    #region Symbols Layout Buttons

    private static List<KeyButton> SymbolsLayoutButtons { get; } =
        new()
        {
            new KeyButton("1", 1),
            new KeyButton("2", 1),
            new KeyButton("3", 1),
            new KeyButton("4", 1),
            new KeyButton("5", 1),
            new KeyButton("6", 1),
            new KeyButton("7", 1),
            new KeyButton("8", 1),
            new KeyButton("9", 1),
            new KeyButton("0", 1),
            
            new KeyButton("+", 2),
            new KeyButton("-", 2),
            new KeyButton("*", 2),
            new KeyButton("/", 2),
            new KeyButton(".", 2),
            new KeyButton(":", 2),
            new KeyButton(",", 2),
            new KeyButton("\"", 2),
            new KeyButton("?", 2),
            new KeyButton("!", 2),
            
            new KeyButton("⇐", 3),
            new KeyButton("⇒", 3),
            new KeyButton("(", 3),
            new KeyButton(")", 3),
            new KeyButton("[", 3),
            new KeyButton("]", 3),
            new KeyButton("{", 3),
            new KeyButton("}", 3),
            new KeyButton("%", 3),
            new KeyButton("@", 3),
        };

    #endregion
}