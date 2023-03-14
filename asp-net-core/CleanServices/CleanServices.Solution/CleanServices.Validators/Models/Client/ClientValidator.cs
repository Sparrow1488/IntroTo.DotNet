using FluentValidation;

namespace CleanServices.Validators.Models.Client;

public class ClientValidator : AbstractValidator<CleanServices.Models.Clients.Client>
{
    public ClientValidator()
    {
        RuleFor(x => x.Info).NotNull().WithMessage("Client should contains Info");
        RuleFor(x => x.Profile).NotNull().WithMessage("Client should contains Profile");
    }
}