using System.Text;

using Microsoft.Identity.Client;

namespace Learn.TemplateStudio.Core.Contracts.Services;

public interface IIdentityCacheService
{
    void SaveMsalToken(byte[] token);

    byte[] ReadMsalToken();
}
