using System.Collections.ObjectModel;
using System.Linq;
using Learn.MultipleFrameworks.Services.Dialogs;

namespace Learn.MultipleFrameworks.ViewModels;

public class AlphabetKeyboardViewModel : DialogContentInjectable
{
    private ObservableCollection<ObservableCollection<KeyButton>> _keyButtonsList;

    public AlphabetKeyboardViewModel()
    {
        InitButtons();
    }

    public ObservableCollection<ObservableCollection<KeyButton>> KeyButtonsList
    {
        get => _keyButtonsList;
        set => SetProperty(ref _keyButtonsList, value);
    }

    private void InitButtons()
    {
        var buttons = new ObservableCollection<KeyButton>
        {
            new("`", 1), // fix
            new("1", 1),
            new("2", 1),
            new("3", 1),
            new("4", 1),
            new("5", 1),
            new("6", 1),
            new("7", 1),
            new("8", 1),
            new("9", 1),
            new("0", 1),
            new("%", 1),
            new("`", 1), // fix
            
            new("q", 2),
            new("w", 2),
            new("e", 2),
            new("r", 2),
            new("t", 2),
            new("z", 2),
            new("u", 2),
            new("i", 2),
            new("o", 2),
            new("p", 2),
            new("/", 2),
            new("(", 2),
            
            new("a", 3),
            new("s", 3),
            new("d", 3),
            new("f", 3),
            new("g", 3),
            new("h", 3),
            new("j", 3),
            new("k", 3),
            new("l", 3),
            new("\"", 3),
            new("!", 3),
            new("'", 3),
            
            new("y", 4),
            new("x", 4),
            new("c", 4),
            new("v", 4),
            new("b", 4),
            new("n", 4),
            new("m", 4),
            new("?", 4),
            new(":", 4),
            new("_", 4),
        };

        var buttonsGroup = buttons
            .OrderBy(x => x.Row)
            .GroupBy(x => x.Row);

        KeyButtonsList = new ObservableCollection<ObservableCollection<KeyButton>>();
        
        foreach (var group in buttonsGroup)
        {
            var rowButtons = group.ToList();
            KeyButtonsList.Add(new ObservableCollection<KeyButton>(rowButtons));
        }
    }
}

public class KeyButton
{
    public KeyButton(string currentSymbol, int row)
    {
        CurrentSymbol = currentSymbol;
        Row = row;
    }
    
    public string CurrentSymbol { get; set; }
    public int Row { get; set; }
}