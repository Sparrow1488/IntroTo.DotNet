using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Learn.PrismFramework.Infrastructure.ViewModels;
using Learn.PrismFramework.Modules.Views;
using Learn.PrismFramework.Services.Interaction;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Ioc;
using Prism.Services.Dialogs;

namespace Learn.PrismFramework.ViewModels;

public class MainWindowViewModel : ViewModel
{
    private readonly IContainerProvider _services;
    private readonly IDialogService _dialogService;
    private readonly IKeyboardInteractionService _keyboardInteraction;
    private readonly IDialogCoordinator _dialogs;
    private string? _message;

    public MainWindowViewModel(
        IContainerProvider services,
        IDialogService dialogService,
        IKeyboardInteractionService keyboardInteraction)
    {
        _services = services;
        _dialogService = dialogService;
        _keyboardInteraction = keyboardInteraction;
        _dialogs = DialogCoordinator.Instance;

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
                    ? "Ответ YES"
                    : "Ответ NO";
            });
    }

    private async void OnShowKeyboard()
    {
        var keyboard = _services.Resolve<KeyboardDialog>();
        await _dialogs.ShowMetroDialogAsync(this, keyboard);
    }
}