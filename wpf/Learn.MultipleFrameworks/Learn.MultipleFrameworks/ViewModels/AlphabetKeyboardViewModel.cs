using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Learn.MultipleFrameworks.Models;
using Learn.MultipleFrameworks.Services.Dialogs;
using Learn.MultipleFrameworks.Services.Loaders;
using Prism.Commands;

namespace Learn.MultipleFrameworks.ViewModels;

public class AlphabetKeyboardViewModel : DialogContentInjectable
{
    private bool _isUpper;
    
    private readonly Dictionary<KeyboardLayout, string> _languagesMap;
    private readonly KeyboardLayoutsManager _keyboardsManager;
    private ObservableCollection<ObservableCollection<KeyButton>> _keyButtonsList;
    private string _nextLanguageLayout;

    public AlphabetKeyboardViewModel(KeyboardLayoutsManager keyboardsManager)
    {
        _keyboardsManager = keyboardsManager;
        
        _languagesMap = new Dictionary<KeyboardLayout, string>
        {
            { KeyboardLayout.EN, "EN" },
            { KeyboardLayout.RU, "RU" }
        };

        KeyButtonsList = new ObservableCollection<ObservableCollection<KeyButton>>();
        
        SetLayoutButtons(KeyboardLayout.EN);

        ChangeLayoutCommand = new DelegateCommand(() => SetLayoutButtons(GetNextLanguageLayout()));
        ChangeUpperCase = new DelegateCommand(() =>
        {
            IsUpperCase = !IsUpperCase;
            
            var buttons = KeyButtonsList.SelectMany(
                x => x.ToList())
                .ToList();
            
            if (IsUpperCase)
                buttons.ForEach(x => x.CurrentSymbol = x.CurrentSymbol.ToUpper());
            if (!IsUpperCase)
                buttons.ForEach(x => x.CurrentSymbol = x.CurrentSymbol.ToLower());

            RenderButtons(buttons);
        });
    }


    public ObservableCollection<ObservableCollection<KeyButton>> KeyButtonsList
    {
        get => _keyButtonsList;
        set => SetProperty(ref _keyButtonsList, value);
    }

    public string NextLayout
    {
        get => _nextLanguageLayout;
        set => SetProperty(ref _nextLanguageLayout, value);
    }
    
    private KeyboardLayout CurrentLayout { get; set; }

    private bool IsUpperCase
    {
        get => _isUpper;
        set => SetProperty(ref _isUpper, value);
    }
    
    public ICommand ChangeLayoutCommand { get; }
    public DelegateCommand ChangeUpperCase { get; }

    private void SetLayoutButtons(KeyboardLayout layout)
    {
        var loadedButtons = KeyboardLayoutsStore.GetLayout(layout);

        CurrentLayout = layout;
        NextLayout = _languagesMap[GetNextLanguageLayout()];

        RenderButtons(loadedButtons);
    }

    private void RenderButtons(List<KeyButton> buttons)
    {
        var buttonsGroup = buttons
            .OrderBy(x => x.Row)
            .GroupBy(x => x.Row);
        
        KeyButtonsList.Clear();
        foreach (var group in buttonsGroup)
        {
            var rowButtons = group.ToList();
            KeyButtonsList.Add(new ObservableCollection<KeyButton>(rowButtons));
        }
    }

    private KeyboardLayout GetNextLanguageLayout()
    {
        var mapsList = _languagesMap.ToList();
        var currentMap = mapsList
            .First(x => x.Key == CurrentLayout);
        var currentMapIndex = mapsList.IndexOf(currentMap);
        
        return currentMapIndex < mapsList.Count - 1
            ? mapsList[currentMapIndex + 1].Key 
            : mapsList.First().Key;
    }
}


