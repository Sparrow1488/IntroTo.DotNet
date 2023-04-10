using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Learn.MultipleFrameworks.Models;
using Learn.MultipleFrameworks.Models.Layouts;
using Learn.MultipleFrameworks.Services.Stores;
using Prism.Commands;

namespace Learn.MultipleFrameworks.ViewModels;

public class AlphabetKeyboardViewModel : KeyboardViewModel
{
    private KeyboardLayout? _layout;
    private string? _nextLayoutName;
    private string? _nextLayoutState;
    
    private readonly List<KeyboardLayout> _layouts = new();
    private readonly Dictionary<LayoutState, List<KeyboardLayout>> _layoutsStore = new();

    public AlphabetKeyboardViewModel()
    {
        AddLayout(KeyboardLayoutsStore.English);
        AddLayout(KeyboardLayoutsStore.Russian);
        AddLayout(KeyboardLayoutsStore.Symbols);
        
        InitLayoutsStore();

        SwitchLayout(CapsLockEnabled);

        SwitchLayoutCommand = new DelegateCommand(() => SwitchLayout(CapsLockEnabled));
        SwitchLayoutStateCommand = new DelegateCommand(SwitchLayoutState);
        SwitchCapsLockCommand = new DelegateCommand(
            () => CapsLockEnabled = SwitchCapsLock(CapsLockEnabled));

        PressSpaceCommand = new DelegateCommand(() => InputSymbol(" "));
        PressBackspaceCommand = new DelegateCommand(() =>
        {
            if (Input.Length >= 1)
            {
                Input = Input.Remove(Input.Length - 1, 1);
            }
        });
    }
    
    protected override string DefaultValue => "";

    public KeyboardLayout? Layout
    {
        get => _layout;
        set => SetProperty(ref _layout, value);
    }

    private LayoutState? State { get; set; }
    private bool CapsLockEnabled { get; set; }

    public string? NextLayoutName
    {
        get => _nextLayoutName;
        set => SetProperty(ref _nextLayoutName, value);
    }
    
    public string? NextLayoutState
    {
        get => _nextLayoutState;
        set => SetProperty(ref _nextLayoutState, value);
    }
    
    public ICommand PressSpaceCommand { get; }
    public ICommand PressBackspaceCommand { get; }
    public ICommand SwitchLayoutCommand { get; }
    public ICommand SwitchLayoutStateCommand { get; }
    public ICommand SwitchCapsLockCommand { get; }

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

    private void SwitchLayoutState()
    {
        State = GetNextLayoutState();
        if (CanSwitchLayout())
        {
            SwitchLayout(CapsLockEnabled);
        }
    }

    private bool CanSwitchLayout()
    {
        return !string.IsNullOrWhiteSpace(NextLayoutState);
    }

    private void SwitchLayout(bool capsLock)
    {
        Layout ??= GetDefaultLayout();
        State ??= Layout.State;
        
        Layout = GetNextLayout();
        SwitchCapsLock(capsLock);
        
        UpdateLayoutName();
        UpdateLayoutState();
    }

    private void UpdateLayoutName()
    {
        var nextLayoutName = GetNextLayout().Type.ToString();
        NextLayoutName = nextLayoutName != Layout!.Type.ToString()
            ? nextLayoutName
            : string.Empty;
    }
    
    private void UpdateLayoutState()
    {
        var nextLayoutState = GetNextLayoutState().ToString();
        NextLayoutState = nextLayoutState != Layout!.State.ToString()
            ? nextLayoutState
            : string.Empty;
    }

    private bool SwitchCapsLock(bool capsLock)
    {
        var keysList = Layout!.Keys.ToList();
        
        if(!capsLock)
            keysList.ForEach(x => x.CurrentSymbol = x.CurrentSymbol.ToUpper());
        else
            keysList.ForEach(x => x.CurrentSymbol = x.CurrentSymbol.ToLower());

        UpdateProperty(ref _layout, Layout, nameof(Layout));

        return !capsLock;
    }

    private void UpdateProperty<T>(ref T variable, T value, string propertyName)
    {
        SetProperty(ref variable!, default, propertyName);
        SetProperty(ref variable, value, propertyName);
    }

    private KeyboardLayout GetDefaultLayout()
    {
        return _layoutsStore.First().Value.First();
    }

    private KeyboardLayout GetNextLayout()
    {
        var availableStateLayouts = _layoutsStore
            .Where(x => x.Key == State)
            .SelectMany(x => x.Value)
            .ToList();

        var currentLayoutIndex = availableStateLayouts.IndexOf(Layout!);
        var nextLayoutIndex = NextIndex(availableStateLayouts, currentLayoutIndex);

        return availableStateLayouts[nextLayoutIndex];
    }

    private LayoutState GetNextLayoutState()
    {
        var currentLayoutState = Layout!.State;

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


