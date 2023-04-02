namespace Learn.MultipleFrameworks.Services.Dialogs;

public interface IDialogContext
{
    bool OpenedInDialog();
    void RequestDialogClose();
}