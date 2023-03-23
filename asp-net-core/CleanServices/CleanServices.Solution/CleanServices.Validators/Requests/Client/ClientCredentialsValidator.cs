using CleanServices.API.Contracts.Client.Requests.Create;
using FluentValidation;

namespace CleanServices.Validators.Requests.Client;

public class ClientCredentialsValidator : AbstractValidator<ClientCredentialsCreateRequest>
{
    public ClientCredentialsValidator()
    {
        RuleFor(x => x.Login)
            .NotNull()
            .MinimumLength(6)
            .WithErrorCode("invalid_login")
            .WithMessage("Minimum 6 characters required");

        RuleFor(x => x.Password)
            .NotNull()
            .MinimumLength(6)
            .WithErrorCode("invalid_password")
            .WithMessage("Minimum 6 characters required");
    }
}