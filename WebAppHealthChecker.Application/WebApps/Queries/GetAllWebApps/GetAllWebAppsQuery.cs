using WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

namespace WebAppHealthChecker.Application.WebApps.Queries.GetAllWebApps
{
    public record GetAllWebAppsQuery : IRequest<List<WebAppWithUserInfoDto>>
    {
    }
}
