using WebAppHealthChecker.Application.Authentication.Queries.Login;

namespace WebAppHealthChecker.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IApplicationUnitOfWork _uow;

    public GetUserQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        => _uow = applicationUnitOfWork;

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _uow.Users
                                   .Select(x => new UserDto
                                   {
                                       Id = x.Id,
                                       FirstName =  x.FirstName,
                                       LastName = x.LastName,
                                       Email = x.Email,
                                       Guid = x.Guid
                                   })
                                   .FirstOrDefaultAsync(x => x.Guid == request.Guid, cancellationToken);
        return result;
    }
}