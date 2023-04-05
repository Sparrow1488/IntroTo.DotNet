using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Learn.MultipleFrameworks.Models;
using Learn.MultipleFrameworks.Services.Dialogs;
using Learn.MultipleFrameworks.Services.Loaders;
using Prism.Commands;
using Prism.Events;

namespace Learn.MultipleFrameworks.ViewModels;

public class AlphabetKeyboardViewModel : DialogContentInjectable
{
    private readonly KeyboardLayoutsManager _keyboardsManager;
    private ObservableCollection<ObservableCollection<KeyButton>> _keyButtonsList;
    private string _nextLanguageLayout;
    private readonly Dictionary<KeyboardLayout, string> _languagesMap;

    public AlphabetKeyboardViewModel(KeyboardLayoutsManager keyboardsManager)
    {
        _keyboardsManager = keyboardsManager;
        
        _languagesMap = new Dictionary<KeyboardLayout, string>
        {
            { KeyboardLayout.EN, "EN" },
            { KeyboardLayout.RU, "RU" }
        };
        
        InitButtons(KeyboardLayout.EN);

        ChangeLayoutCommand = new DelegateCommand(() => InitButtons(GetNextLanguageLayout()));
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
    
    public ICommand ChangeLayoutCommand { get; }

    private void InitButtons(KeyboardLayout layout)
    {
        // var loadedButtons = _keyboardsManager.LoadLayout(layout);
        var loadedButtons = KeyboardLayoutsStore.GetLayout(layout);

        var buttonsGroup = loadedButtons
            .OrderBy(x => x.Row)
            .GroupBy(x => x.Row);

        CurrentLayout = layout;
        NextLayout = _languagesMap[GetNextLanguageLayout()];

        KeyButtonsList = new ObservableCollection<ObservableCollection<KeyButton>>();
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


