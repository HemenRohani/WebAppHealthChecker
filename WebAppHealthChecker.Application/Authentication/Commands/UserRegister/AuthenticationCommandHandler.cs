using WebAppHealthChecker.Application.Common.Interfaces;
using WebAppHealthChecker.Domain.Entities;

namespace WebAppHealthChecker.Application.Authentication.Commands.UserRegister;

public class AuthenticationCommandHandler : IRequestHandler<UserRegisterCommand, Guid>
{
    private readonly IApplicationUnitOfWork _uow;

    public AuthenticationCommandHandler(IApplicationUnitOfWork unitOfWork)
         => _uow = unitOfWork;

    public async Task<Guid> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var model = new User
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        _uow.Users.Add(model);
        await _uow.SaveChangesAsync(cancellationToken);
        return model.Guid;
    }
}