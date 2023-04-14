using System;
using System.ComponentModel;
using System.Windows;
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

public abstract class KeyboardViewModel : DialogContentInjectable, IDataErrorInfo
{
    private string _input = string.Empty;
    private Visibility _passwordVisibility = Visibility.Collapsed;
    private Visibility _textVisibility = Visibility.Visible;

    public event Action<string?>? PressKeyButton;

    public KeyboardViewModel()
    {
        ResetCommand = new DelegateCommand(ResetInput);
        SubmitCommand = new DelegateCommand(SubmitInput);
        InputSymbolCommand = new DelegateCommand<string>(InputSymbol);

        Settings = Container.Resolve<KeyboardSettingsProvider>()?.KeyboardSettings;

        if (Settings is {IsPassword: true})
        {
            MaskChar = '*';
            TextVisibility = Visibility.Collapsed;
            PasswordVisibility = Visibility.Visible;
        }

        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(Input))
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(InputMask)));
        };

        Input = string.Empty;
    }

    public Visibility PasswordVisibility
    {
        get => _passwordVisibility;
        set => SetProperty(ref _passwordVisibility, value);
    }
    public Visibility TextVisibility
    {
        get => _textVisibility;
        set => SetProperty(ref _textVisibility, value);
    }
    
    public KeyboardSettings? Settings { get; }

    protected abstract string DefaultValue { get; }
    protected virtual char? MaskChar { get; set; }

    public string Error => string.Empty;

    public string this[string input]
    {
        get
        {
            const string @default = "";
            if (Settings?.InputValidationFunc == null 
                || input != nameof(Input)
                || input != nameof(InputMask))
                return @default;

            var validation = Settings.InputValidationFunc.Invoke(Input);
            return validation?.ErrorMessage ?? @default;
        }
    }
    
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
        PressKeyButton?.Invoke(symbol);
    }

    protected virtual bool UseInputMask()
    {
        return MaskChar != null;
    }
}