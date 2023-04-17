using System.Linq;

namespace Learn.MultipleFrameworks.Services.Utilities;

public static class InputUtilities
{
    public static string? TrimStartZero(string? input)
    {
        if (input?.Length > 1 && input[0] == '0')
        {
            return string.Join("", input.Skip(1).ToArray());
        }

        return input;
    }
}