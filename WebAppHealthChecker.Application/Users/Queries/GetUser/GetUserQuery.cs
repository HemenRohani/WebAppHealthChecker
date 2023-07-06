namespace WebAppHealthChecker.Application.Users.Queries.GetUser;

public record GetUserQuery : IRequest<GetUserDto>
{
    public Guid Guid { get; set; }
}