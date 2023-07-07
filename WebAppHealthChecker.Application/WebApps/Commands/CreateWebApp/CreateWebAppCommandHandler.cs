using WebAppHealthChecker.Application.Common;
using WebAppHealthChecker.Application.Users.Queries.GetUser;
using WebAppHealthChecker.Domain.Entities;

namespace WebAppHealthChecker.Application.WebApps.Commands.CreateWebApp;

public class CreateWebAppCommandHandler : IRequestHandler<CreateWebAppCommand, Guid>
{
    private readonly IApplicationUnitOfWork _uow;
    private readonly UserData _userData;
    public CreateWebAppCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, UserData userData)
    {
        _uow = applicationUnitOfWork;
        _userData = userData;
    }

    public async Task<Guid> Handle(CreateWebAppCommand request, CancellationToken cancellationToken)
    {
        var webApp = new WebApp
        {
            Name = request.Name,
            CheckInterval = request.CheckInterval,
            URL = request.URL,
            UserId = _userData.Id
        };
        _uow.WebApps.Add(webApp);
        await _uow.SaveChangesAsync(cancellationToken);
        return webApp.Guid;
    }
}