using System.Windows.Input;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Learn.PrismFramework.Modules.Views;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace Learn.PrismFramework.ViewModels;

public class MainWindowViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    private string? _message;

    public MainWindowViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
        
        ShowQuestionBoxCommand = new DelegateCommand(OnShowQuestionBox);
    }

    public string? Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }
    
    public ICommand ShowQuestionBoxCommand { get; }

    private void OnShowQuestionBox()
    {
        var parameters = new DialogParameters
        {
            { "title", "Ну как там с деньгами" },
            { "question", "Не хотите вложить деньги в наш капитал?" },
        };
        
        _dialogService.ShowDialog(
            nameof(QuestionDialog),
            parameters,
            result =>
            {
                Message = result.Result == ButtonResult.Yes
                    ? "Пользователь - ровный чел"
                    : "Пользователь гондон";
            });
    }
}