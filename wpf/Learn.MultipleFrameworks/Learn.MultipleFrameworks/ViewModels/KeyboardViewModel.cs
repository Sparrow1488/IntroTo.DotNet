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
    private string _input = null!;
    private Visibility _passwordVisibility = Visibility.Collapsed;
    private Visibility _textVisibility = Visibility.Visible;

    public KeyboardViewModel()
    {
        ResetCommand = new DelegateCommand(ResetInput);
        SubmitCommand = new DelegateCommand(SubmitInput);
        InputSymbolCommand = new DelegateCommand<string>(InputSymbol);

        Settings = Container.Resolve<KeyboardSettingsProvider>()?.KeyboardSettings;

        if (Settings is {IsPassword: true})
        {
            PasswordBoxVisibility = Visibility.Visible;
            TextBoxVisibility = Visibility.Collapsed;
        }
    }
    
    public KeyboardSettings? Settings { get; }

    protected abstract string DefaultValue { get; }

    public Visibility PasswordBoxVisibility
    {
        get => _passwordVisibility;
        set => SetProperty(ref _passwordVisibility, value);
    }
    
    public Visibility TextBoxVisibility
    {
        get => _textVisibility;
        set => SetProperty(ref _textVisibility, value);
    }

    public string Error => string.Empty;

    public string this[string input]
    {
        get
        {
            const string @default = "";

            if (Settings?.InputValidationFunc == null
                || input != nameof(Input)
                || string.IsNullOrWhiteSpace(Input))
            {
                return @default;
            }
            
            var validation = Settings.InputValidationFunc.Invoke(Input);
            return validation.ErrorMessage ?? @default;
        }
    }
    
    public string Input
    {
        get => _input;
        set
        {
            // TODO: not works
            if (!CheckInputValidation(value))
            {
                SetProperty(ref _input, _input);
                return;                
            }
            
            var usePassword = Settings?.IsPassword ?? false;
            if (string.IsNullOrWhiteSpace(value) && !usePassword)
                SetProperty(ref _input, DefaultValue);
            else
                SetProperty(ref _input, value);
        }
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

    protected virtual bool CheckInputValidation(string input)
    {
        return true;
    }
}