using WebAppHealthChecker.Application.Common;
using WebAppHealthChecker.Application.Users.Queries.GetUser;
using WebAppHealthChecker.Domain.Entities;

namespace WebAppHealthChecker.Application.WebApps.Commands.UpdateWebApp;

public class UpdateWebAppCommandHandler : IRequestHandler<UpdateWebAppCommand, Guid>
{
    private readonly IApplicationUnitOfWork _uow;
    private readonly UserData _userData;
    public UpdateWebAppCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, UserData userData)
    {
        _uow = applicationUnitOfWork;
        _userData = userData;
    }

    public async Task<Guid> Handle(UpdateWebAppCommand request, CancellationToken cancellationToken)
    {
        var webApp = await _uow.WebApps.FirstAsync(x => x.Guid == request.Guid, cancellationToken);

        webApp.Name = request.Name;
        webApp.CheckInterval = request.CheckInterval;
        webApp.URL = request.URL;

        await _uow.SaveChangesAsync(cancellationToken);

        return webApp.Guid;
    }
}