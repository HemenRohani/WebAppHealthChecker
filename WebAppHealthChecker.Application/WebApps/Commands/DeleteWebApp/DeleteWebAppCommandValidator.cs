using FluentValidation;

namespace WebAppHealthChecker.Application.WebApps.Commands.DeleteWebApp;

public class DeleteWebAppCommandValidator : AbstractValidator<DeleteWebAppCommand>
{
    public DeleteWebAppCommandValidator()
    {
        RuleFor(u => u.Guid)
               .NotEmpty().WithMessage("this field is required");
    }
}