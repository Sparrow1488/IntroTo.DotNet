using Learn.TemplateStudio.ViewModels;

namespace Learn.TemplateStudio.Contracts.Services;

public interface IUserDataService
{
    event EventHandler<UserViewModel> UserDataUpdated;

    void Initialize();

    UserViewModel GetUser();
}
