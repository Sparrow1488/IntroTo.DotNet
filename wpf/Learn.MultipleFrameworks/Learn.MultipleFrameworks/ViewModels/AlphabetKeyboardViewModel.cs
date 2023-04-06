using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Learn.MultipleFrameworks.Models;
using Learn.MultipleFrameworks.Models.Layouts;
using Learn.MultipleFrameworks.Services.Dialogs;
using Learn.MultipleFrameworks.Services.Loaders;
using Prism.Commands;

namespace Learn.MultipleFrameworks.ViewModels;

public class AlphabetKeyboardViewModel : DialogContentInjectable
{
    private bool _isUpper;
    private KeyboardLayout _layout;
    private string _nextLayoutKeyTextKeyText;
    
    private readonly List<KeyboardLayout> _layouts = new();
    private readonly Dictionary<LayoutState, List<KeyboardLayout>> _layoutsStore = new();

    public AlphabetKeyboardViewModel()
    {
        AddLayout(KeyboardLayoutsStore.English);
        AddLayout(KeyboardLayoutsStore.Russian);
        
        InitLayoutsStore();

        SwitchLayout();

        SwitchLayoutCommand = new DelegateCommand(SwitchLayout);
        
        // SwitchUpperCase = new DelegateCommand(() =>
        // {
        //     IsUpperCase = !IsUpperCase;
        //     
        //     var buttons = KeyButtonsList.SelectMany(
        //         x => x.ToList())
        //         .ToList();
        //     
        //     if (IsUpperCase)
        //         buttons.ForEach(x => x.CurrentSymbol = x.CurrentSymbol.ToUpper());
        //     if (!IsUpperCase)
        //         buttons.ForEach(x => x.CurrentSymbol = x.CurrentSymbol.ToLower());
        //
        //     ShowLayout(buttons);
        // });
    }


    public KeyboardLayout Layout
    {
        get => _layout;
        set => SetProperty(ref _layout, value);
    }

    public string NextLayoutKeyText
    {
        get => _nextLayoutKeyTextKeyText;
        set => SetProperty(ref _nextLayoutKeyTextKeyText, value);
    }
    
    private bool IsUpperCase
    {
        get => _isUpper;
        set => SetProperty(ref _isUpper, value);
    }
    
    public ICommand SwitchLayoutCommand { get; }
    public ICommand SwitchUpperCase { get; }

    private void AddLayout(KeyboardLayout layout)
    {
        _layouts.Add(layout);
    }

    private void InitLayoutsStore()
    {
        foreach (var grouping in _layouts.GroupBy(x => x.State))
        {
            var layoutsList = grouping.ToList();
            _layoutsStore.Add(grouping.Key, layoutsList);
        }
    }

    private void SwitchLayout()
    {
        Layout = GetNextLayout();
        NextLayoutKeyText = GetNextLayout().Type.ToString();
    }

    private KeyboardLayout GetNextLayout()
    {
        var currentLayoutState = Layout.State;
        var availableStateLayouts = _layoutsStore
            .Where(x => x.Key == currentLayoutState)
            .SelectMany(x => x.Value)
            .ToList();

        var currentLayoutIndex = availableStateLayouts.IndexOf(Layout);
        var nextLayoutIndex = currentLayoutIndex < availableStateLayouts.Count - 1
            ? currentLayoutIndex + 1
            : 0;

        return availableStateLayouts[nextLayoutIndex];
    }
}


