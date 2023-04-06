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
    private string _nextLayoutName;
    private string _nextLayoutType;
    
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

    public string NextLayoutName
    {
        get => _nextLayoutName;
        set => SetProperty(ref _nextLayoutName, value);
    }
    
    public string NextLayoutType
    {
        get => _nextLayoutType;
        set => SetProperty(ref _nextLayoutType, value);
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
        Layout ??= GetDefaultLayout();
        
        Layout = GetNextLayout();
        NextLayoutName = GetNextLayout().Type.ToString();
        NextLayoutType = GetNextLayoutState().ToString();
    }

    private KeyboardLayout GetDefaultLayout()
    {
        return _layoutsStore.First().Value.First();
    }

    private KeyboardLayout GetNextLayout()
    {
        var currentLayoutState = Layout.State;
        
        var availableStateLayouts = _layoutsStore
            .Where(x => x.Key == currentLayoutState)
            .SelectMany(x => x.Value)
            .ToList();

        var currentLayoutIndex = availableStateLayouts.IndexOf(Layout);
        var nextLayoutIndex = NextIndex(availableStateLayouts, currentLayoutIndex);

        return availableStateLayouts[nextLayoutIndex];
    }

    private LayoutState GetNextLayoutState()
    {
        var currentLayoutState = Layout.State;

        var keys = _layoutsStore.Keys.ToList();
        var currentStateIndex = keys.IndexOf(currentLayoutState);
        var nextStateIndex = NextIndex(keys, currentStateIndex);

        return keys[nextStateIndex];
    }

    private static int NextIndex<T>(List<T> items, int currentIndex)
    {
        return currentIndex < items.Count - 1
            ? currentIndex + 1
            : 0;
    }
}


