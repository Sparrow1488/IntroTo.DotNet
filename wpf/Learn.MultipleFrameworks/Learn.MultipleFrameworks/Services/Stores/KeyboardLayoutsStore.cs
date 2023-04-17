using System.Collections.Generic;
using Learn.MultipleFrameworks.Models;
using Learn.MultipleFrameworks.Models.Layouts;

namespace Learn.MultipleFrameworks.Services.Stores;

public static class KeyboardLayoutsStore
{
    public static KeyboardLayout English => new EngKeyboardLayout(EnglishLayoutButtons);
    public static KeyboardLayout Russian => new RusKeyboardLayout(RussianLayoutButtons);
    public static KeyboardLayout Symbols => new SymKeyboardLayout(SymbolsLayoutButtons);

    #region English Layout Buttons

    private static List<KeyButton> EnglishLayoutButtons { get; } =
        new()
        {
            new KeyButton("q", 1),
            new KeyButton("w", 1),
            new KeyButton("e", 1),
            new KeyButton("r", 1),
            new KeyButton("t", 1),
            new KeyButton("y", 1),
            new KeyButton("u", 1),
            new KeyButton("i", 1),
            new KeyButton("o", 1),
            new KeyButton("p", 1),
            new KeyButton("(", 1),
            new KeyButton(")", 1),
                    
            new KeyButton("a", 2),
            new KeyButton("s", 2),
            new KeyButton("d", 2),
            new KeyButton("f", 2),
            new KeyButton("g", 2),
            new KeyButton("h", 2),
            new KeyButton("j", 2),
            new KeyButton("k", 2),
            new KeyButton("l", 2),
            new KeyButton(";", 2),
            new KeyButton(",", 2),
                    
            new KeyButton("z", 3),
            new KeyButton("x", 3),
            new KeyButton("c", 3),
            new KeyButton("v", 3),
            new KeyButton("b", 3),
            new KeyButton("n", 3),
            new KeyButton("m", 3),
            new KeyButton("?", 3),
            new KeyButton(":", 3),
            new KeyButton("_", 3),
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
            new KeyButton("о", 2),
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
            new KeyButton("№", 1),
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
            new KeyButton("#", 1),
            
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
            new KeyButton("^", 2),
            
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