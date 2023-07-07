using FluentValidation;

namespace WebAppHealthChecker.Application.WebApps.Commands.CreateWebApp;

public class CreateWebAppCommandValidator : AbstractValidator<CreateWebAppCommand>
{
    public CreateWebAppCommandValidator()
    {
        RuleFor(u => u.Name)
               .NotEmpty().WithMessage("this field is required")
               .MaximumLength(50).WithMessage("first name must be less than 50");

        RuleFor(u => u.URL)
               .NotEmpty().WithMessage("this field is required")
               .Matches("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$").WithMessage("URL is not valid");

        RuleFor(u => u.CheckInterval)
            .GreaterThan(0)
            .WithMessage("CheckInterval must be greater than Zero");
    }
}