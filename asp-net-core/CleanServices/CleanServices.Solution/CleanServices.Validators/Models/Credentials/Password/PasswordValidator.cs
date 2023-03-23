using FluentValidation.Results;

namespace CleanServices.Validators.Models.Credentials.Password;

public class PasswordValidator : IPasswordValidator
{
    public Task<ValidationResult> ValidateAsync(string password)
    {
        if (password.Length < 6)
        {
            var failure = new ValidationFailure("Password", "Min 6 characters required")
            {
                ErrorCode = "too_short_password"
            };
            return Task.FromResult(new ValidationResult(new[] { failure }));
        }

        return Task.FromResult(new ValidationResult());
    }
}