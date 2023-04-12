using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using Learn.MultipleFrameworks.Events;
using Learn.MultipleFrameworks.Events.Models;
using Learn.MultipleFrameworks.Models.Settings;
using Learn.MultipleFrameworks.Services.Dialogs;
using Learn.MultipleFrameworks.Services.Keyboards;
using Prism.Commands;
using Prism.Ioc;

namespace Learn.MultipleFrameworks.ViewModels;

#region Disabled inspections

// ReSharper disable once VirtualMemberCallInConstructor
// ReSharper disable once PublicConstructorInAbstractClass
// ReSharper disable once MemberCanBeProtected.Global
// ReSharper disable once MemberCanBePrivate.Global
// ReSharper disable once UnusedAutoPropertyAccessor.Global
// ReSharper disable file VirtualMemberCallInConstructor

#endregion

public abstract class KeyboardViewModel : DialogContentInjectable
{
    private string _input;
    private FlowDocument _document;

    public KeyboardViewModel()
    {
        ResetCommand = new DelegateCommand(ResetInput);
        SubmitCommand = new DelegateCommand(SubmitInput);
        InputSymbolCommand = new DelegateCommand<string>(InputSymbol);

        Settings = Container.Resolve<KeyboardSettingsProvider>()?.KeyboardSettings;

        if (Settings is {IsPassword: true})
            MaskChar = '*';

        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(Input))
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(InputMask)));
        };

        Input = string.Empty;
    }

    public KeyboardSettings? Settings { get; }

    protected abstract string DefaultValue { get; }
    protected virtual char? MaskChar { get; set; }
    
    public string Input
    {
        get => _input;
        set
        {
            var usePassword = Settings?.IsPassword ?? false;
            if (string.IsNullOrWhiteSpace(value) && !usePassword)
                SetProperty(ref _input, DefaultValue);
            else
                SetProperty(ref _input, value);
        }
    }
    public string InputMask
    {
        get => UseInputMask() 
            ? new string((char) MaskChar!, Input.Length) 
            : Input;
        set => Input = value;
    }
    
    public ICommand InputSymbolCommand { get; }
    public ICommand SubmitCommand { get; }
    public ICommand ResetCommand { get; }

    private void OnSubmitInput(bool openedInDialog)
    {
        var input = new KeyboardInput(Input, openedInDialog);
        Aggregator.GetEvent<SubmitKeyboardInputEvent>().Publish(input);
    }
    
    private void ResetInput()
    {
        Input = DefaultValue;
    }
    
    private void SubmitInput()
    {
        var openedInDialog = RequestDialogClose();
        
        OnSubmitInput(openedInDialog);
        ResetInput();
    }

    protected virtual void InputSymbol(string? symbol)
    {
        Input += symbol;
    }

    protected virtual bool UseInputMask()
    {
        return MaskChar != null;
    }
}