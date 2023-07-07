using FluentValidation;

namespace WebAppHealthChecker.Application.WebApps.Commands.UpdateWebApp;

public class UpdateWebAppCommandValidator : AbstractValidator<UpdateWebAppCommand>
{
    public UpdateWebAppCommandValidator()
    {
        RuleFor(u => u.Guid)
               .NotEmpty().WithMessage("this field is required");

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