using FluentValidation;

namespace WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

public class GetUserWebAppsQueryValidator : AbstractValidator<GetUserWebAppsQuery>
{
    public GetUserWebAppsQueryValidator()
    {
        RuleFor(u => u.UserGuid)
            .NotEmpty()
            .WithMessage("this field is Required");
    }
}