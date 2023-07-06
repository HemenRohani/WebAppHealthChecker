namespace WebAppHealthChecker.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
{
    private readonly IApplicationUnitOfWork _uow;

    public GetUserQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        => _uow = applicationUnitOfWork;

    public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _uow.Users
                                   .Select(x => new GetUserDto
                                   {
                                       Email = x.Email,
                                       Guid = x.Guid
                                   })
                                   .FirstOrDefaultAsync(x => x.Guid == request.Guid, cancellationToken);
        return result;
    }
}