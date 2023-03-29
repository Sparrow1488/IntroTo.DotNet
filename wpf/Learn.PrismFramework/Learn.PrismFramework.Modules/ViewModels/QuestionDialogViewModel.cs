using System;
using System.Windows.Input;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace Learn.PrismFramework.Modules.ViewModels;

public class QuestionDialogViewModel : ViewModel, IDialogAware
{
    private string? _title;
    private string? _question;
    
    public event Action<IDialogResult>? RequestClose;

    public QuestionDialogViewModel()
    {
        CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
    }

    public string? Title
    {
        get => _title;
        private set => SetProperty(ref _title, value);
    }
    public string? Question
    {
        get => _question;
        private set => SetProperty(ref _question, value);
    }

    public ICommand CloseDialogCommand { get; }

    public bool CanCloseDialog() => true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        var title = parameters.GetValue<string>("title");
        var question = parameters.GetValue<string>("question");

        Title = title;
        Question = question;
    }

    private void CloseDialog(string parameter)
    {
        var result = parameter == "Yes"
            ? ButtonResult.Yes
            : ButtonResult.No;
        
        RequestClose?.Invoke(new DialogResult(result));
    }
}