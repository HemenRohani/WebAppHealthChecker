using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppHealthChecker.Application.Common;
using WebAppHealthChecker.Application.WebApps.Commands.UpdateWebApp;

namespace WebAppHealthChecker.Application.WebApps.Commands.UpdateWebAppStatus
{
    internal class UpdateWebAppStatusCommandHandler : IRequestHandler<UpdateWebAppStatusCommand, bool>
    {
        private readonly IApplicationUnitOfWork _uow;
        private readonly UserData _userData;
        public UpdateWebAppStatusCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, UserData userData)
        {
            _uow = applicationUnitOfWork;
            _userData = userData;
        }

        public async Task<bool> Handle(UpdateWebAppStatusCommand request, CancellationToken cancellationToken)
        {
            var webApp = await _uow.WebApps.FirstAsync(x => x.Guid == request.Guid, cancellationToken);

            webApp.LastStatusCode = request.LastStatusCode;
            webApp.LastCheck = request.LastCheck;

            await _uow.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
