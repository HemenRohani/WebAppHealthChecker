using FluentValidation;

namespace WebAppHealthChecker.Application.Authentication.Commands.UserRegister;

public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
{

    private readonly IApplicationUnitOfWork _uow;

    public UserRegisterCommandValidator(IApplicationUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;

        RuleFor(u => u.FirstName)
               .NotEmpty().WithMessage("this field is required")
               .MaximumLength(50).WithMessage("first name must be less than 50");

        RuleFor(u => u.LastName)
               .NotEmpty().WithMessage("this field is required")
               .MaximumLength(50).WithMessage("last name must be less than 50");

        RuleFor(u => u.Email)
               .NotEmpty().WithMessage("this field is required")
               .EmailAddress().WithMessage("Email is not valid")
               .Must(x => !IsDuplicate(x)).WithMessage("This Email is used");
    }

    private bool IsDuplicate(string email)
    {
        return _uow.Users.Any(x => x.Email == email);
    }
}