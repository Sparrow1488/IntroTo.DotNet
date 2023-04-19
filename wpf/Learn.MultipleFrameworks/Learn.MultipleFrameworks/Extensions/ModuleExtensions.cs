using Learn.MultipleFrameworks.Modules;
using Prism.Modularity;

namespace Learn.MultipleFrameworks.Extensions;

public static class ModuleExtensions
{
    public static IModuleCatalog AddKeyboardsModules(this IModuleCatalog modules)
    {
        modules.AddModule<AlphabetKeyboardModule>();
        modules.AddModule<NumericKeyboardModule>();
        modules.AddModule<LimitsKeyboardModule>();

        return modules;
    }
}