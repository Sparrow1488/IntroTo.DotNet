using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Services.Dialogs;
using Prism.Commands;

namespace Learn.MultipleFrameworks.ViewModels;

#region Disabled inspections

// ReSharper disable once VirtualMemberCallInConstructor
// ReSharper disable once PublicConstructorInAbstractClass
// ReSharper disable once MemberCanBeProtected.Global
// ReSharper disable once MemberCanBePrivate.Global
// ReSharper disable once UnusedAutoPropertyAccessor.Global


#endregion

public abstract class KeyboardViewModel : BindableDialogContentManager
{
    private string _input;
    
    public KeyboardViewModel()
    {
        _input = DefaultValue;
        
        ResetCommand = new DelegateCommand(ResetInput);
        SubmitCommand = new DelegateCommand(SubmitInput);
        InputSymbolCommand = new DelegateCommand<string>(InputSymbol);
    }
    
    protected abstract string DefaultValue { get; }
    public string Input
    {
        get => _input;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                SetProperty(ref _input, DefaultValue);
            else
                SetProperty(ref _input, value);
        }
    }
    
    public ICommand InputSymbolCommand { get; }
    public ICommand SubmitCommand { get; }
    public ICommand ResetCommand { get; }

    protected virtual void OnSubmitInput()
    {
        var input = new KeyboardInput(Input);
        Aggregator.GetEvent<SubmitKeyboardInputEvent>().Publish(input);
    }
    
    private void ResetInput()
    {
        Input = DefaultValue;
    }
    
    private void SubmitInput()
    {
        OnSubmitInput();
        
        ResetInput();
        RequestDialogClose();
    }

    protected virtual void InputSymbol(string symbol)
    {
        Input += symbol;
    }
}