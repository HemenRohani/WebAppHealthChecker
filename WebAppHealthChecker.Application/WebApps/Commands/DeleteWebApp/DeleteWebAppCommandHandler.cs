using WebAppHealthChecker.Application.Common;
using WebAppHealthChecker.Application.Users.Queries.GetUser;
using WebAppHealthChecker.Domain.Entities;

namespace WebAppHealthChecker.Application.WebApps.Commands.DeleteWebApp;

public class DeleteWebAppCommandHandler : IRequestHandler<DeleteWebAppCommand, Guid>
{
    private readonly IApplicationUnitOfWork _uow;
    public DeleteWebAppCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
    {
        _uow = applicationUnitOfWork;
    }

    public async Task<Guid> Handle(DeleteWebAppCommand request, CancellationToken cancellationToken)
    {
        var webApp = await _uow.WebApps.FirstAsync(x => x.Guid == request.Guid, cancellationToken);
        _uow.WebApps.Remove(webApp);
        await _uow.SaveChangesAsync();
        return request.Guid;
    }
}