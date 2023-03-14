namespace CleanServices.Validators.Models.Credentials.Password.Results;

public class PasswordValidationResult
{
    public PasswordValidationResult(bool isValid, Dictionary<string, string> errors)
    {
        IsValid = isValid;
        Errors = errors;
    }
    
    public bool IsValid { get; }
    public Dictionary<string, string> Errors { get; }
}