using FluentValidation.Results;

namespace CleanServices.Validators.Models.Credentials.Password;

public interface IPasswordValidator
{
    Task<ValidationResult> ValidateAsync(string password);
}