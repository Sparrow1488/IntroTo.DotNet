using Learn.TemplateStudio.Core.Models;

namespace Learn.TemplateStudio.Core.Contracts.Services;

public interface IMicrosoftGraphService
{
    Task<User> GetUserInfoAsync(string accessToken);

    Task<string> GetUserPhoto(string accessToken);
}
