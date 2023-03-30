using System.Windows.Input;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Learn.PrismFramework.Modules.ViewModels;
using Learn.PrismFramework.Modules.Views;
using Learn.PrismFramework.Services.Interaction;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace Learn.PrismFramework.ViewModels;

public class MainWindowViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    private readonly IKeyboardInteractionService _keyboardInteraction;
    private readonly IDialogCoordinator _dialogCoordinator;
    private string? _message;

    public MainWindowViewModel(
        IDialogService dialogService,
        IKeyboardInteractionService keyboardInteraction)
    {
        _dialogService = dialogService;
        _keyboardInteraction = keyboardInteraction;
        _dialogCoordinator = DialogCoordinator.Instance;

        ShowQuestionBoxCommand = new DelegateCommand(OnShowQuestionBox);
        ShowKeyboardCommand = new DelegateCommand(OnShowKeyboard);
    }

    public string? Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }
    
    public ICommand ShowQuestionBoxCommand { get; }
    public ICommand ShowKeyboardCommand { get; }

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

    private async void OnShowKeyboard()
    {
        // await _dialogCoordinator.ShowMessageAsync(this, "Title", "Text");

        var keyboard = new KeyboardViewModel();
        var dialogViewModel = new KeyboardDialogViewModel(keyboard);
        await _dialogCoordinator.ShowMetroDialogAsync(this, new KeyboardDialog(dialogViewModel));
        
        // _keyboardInteraction.ShowKeyboard(input => Message = input);
    }
}