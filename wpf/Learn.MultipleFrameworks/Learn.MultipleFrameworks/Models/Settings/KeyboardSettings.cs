using System;

namespace Learn.MultipleFrameworks.Models.Settings;

public class KeyboardSettings
{
    public bool IsPassword { get; set; }
    public Func<string, ValidationResult>? InputValidationFunc { get; set; }
}