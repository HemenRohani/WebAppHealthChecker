namespace WebAppHealthChecker.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
{
    private readonly IApplicationUnitOfWork _uow;

    public LoginQueryHandler(IApplicationUnitOfWork unitOfWork)
         => _uow = unitOfWork;

    public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _uow.Users.Where(x => x.Email == request.Email
                                             && x.Password == request.Password)
                                  .Select(x => new UserDto
                                  {
                                      Id = x.Id,
                                      Guid = x.Guid,
                                      Email = x.Email,
                                      FirstName = x.FirstName,
                                      LastName = x.LastName
                                  })
                                  .FirstOrDefaultAsync(cancellationToken);
        return user ?? null;
    }
}

 