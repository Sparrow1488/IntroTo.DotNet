using System.Collections;
using System.IO;

using Learn.TemplateStudio.Contracts.Services;
using Learn.TemplateStudio.Core.Contracts.Services;
using Learn.TemplateStudio.Models;

namespace Learn.TemplateStudio.Services;

public class PersistAndRestoreService : IPersistAndRestoreService
{
    private readonly IFileService _fileService;
    private readonly AppConfig _appConfig;
    private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public PersistAndRestoreService(IFileService fileService, AppConfig appConfig)
    {
        _fileService = fileService;
        _appConfig = appConfig;
    }

    public void PersistData()
    {
        if (App.Current.Properties != null)
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.AppPropertiesFileName;
            _fileService.Save(folderPath, fileName, App.Current.Properties);
        }
    }

    public void RestoreData()
    {
        var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
        var fileName = _appConfig.AppPropertiesFileName;
        var properties = _fileService.Read<IDictionary>(folderPath, fileName);
        if (properties != null)
        {
            foreach (DictionaryEntry property in properties)
            {
                App.Current.Properties.Add(property.Key, property.Value);
            }
        }
    }
}
