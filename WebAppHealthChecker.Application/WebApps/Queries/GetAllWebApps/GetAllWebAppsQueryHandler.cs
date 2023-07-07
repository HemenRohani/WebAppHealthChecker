using WebAppHealthChecker.Application.WebApps.Queries.GetAllWebApps;

namespace WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

public class GetAllWebAppsQueryHandler : IRequestHandler<GetAllWebAppsQuery, List<WebAppWithUserInfoDto>>
{
    private readonly IApplicationUnitOfWork _uow;

    public GetAllWebAppsQueryHandler(IApplicationUnitOfWork unitOfWork)
        => _uow = unitOfWork;

    public async Task<List<WebAppWithUserInfoDto>> Handle(GetAllWebAppsQuery request, CancellationToken cancellationToken)
    {
        var result = await _uow.WebApps.Include(x => x.User).Select(x => new WebAppWithUserInfoDto
        {
            UserEmail = x.User.Email,
            UserFirstName = x.User.FirstName,
            UserLastName = x.User.LastName,
            CheckInterval = x.CheckInterval,
            Name = x.Name,
            URL = x.URL
        }).ToListAsync();
        return result;
    }
}