using WebAppHealthChecker.Application.Authentication.Queries.Login;

namespace WebAppHealthChecker.Application.Users.Queries.GetUser;

public record GetUserQuery : IRequest<UserDto>
{
    public Guid Guid { get; set; }
}