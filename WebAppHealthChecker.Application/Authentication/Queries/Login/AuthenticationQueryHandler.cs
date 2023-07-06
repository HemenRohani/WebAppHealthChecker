using WebAppHealthChecker.Application.Common.Interfaces;

namespace WebAppHealthChecker.Application.Authentication.Queries.Login;

public class AuthenticationQueryHandler : IRequestHandler<LoginQuery, UserDto>
{
    private readonly IApplicationUnitOfWork _uow;

    public AuthenticationQueryHandler(IApplicationUnitOfWork unitOfWork)
         => _uow = unitOfWork;

    public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _uow.Users.Where(x => x.Email == request.Email
                                             && x.Password == request.Password)
                                  .Select(x => new UserDto
                                  {
                                      Guid = x.Guid,
                                      Email = x.Email,
                                      FirstName = x.FirstName,
                                      LastName = x.LastName
                                  })
                                  .FirstOrDefaultAsync(cancellationToken);
        return user ?? null;
    }
}

 