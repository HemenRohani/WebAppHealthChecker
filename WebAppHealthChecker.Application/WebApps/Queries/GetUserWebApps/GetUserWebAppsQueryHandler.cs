using WebAppHealthChecker.Application.Authentication.Queries.Login;

namespace WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

public class GetUserWebAppsQueryHandler : IRequestHandler<GetUserWebAppsQuery, List<WebAppDto>>
{
    private readonly IApplicationUnitOfWork _uow;

    public GetUserWebAppsQueryHandler(IApplicationUnitOfWork unitOfWork)
        => _uow = unitOfWork;

    public async Task<List<WebAppDto>> Handle(GetUserWebAppsQuery request, CancellationToken cancellationToken)
    {
        var result = await _uow.WebApps.Where(x => x.User.Guid == request.UserGuid).Select(x => new WebAppDto
        {
            Guid = x.Guid,
            CheckInterval = x.CheckInterval,
            LastCheck = x.LastCheck,
            LastStatusCode = x.LastStatusCode,
            Name = x.Name,
            URL = x.URL
        }).ToListAsync();
        return result;
    }
}