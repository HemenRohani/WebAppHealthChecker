using FluentValidation;

namespace WebAppHealthChecker.Application.Users.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(u => u.Guid)
            .NotEmpty()
            .WithMessage("this field is Required");
    }
}