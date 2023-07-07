using WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

namespace WebAppHealthChecker.Application.WebApps.Queries.GetWebApp
{
    public record GetWebAppQuery : IRequest<WebAppDto>
    {
        public Guid Guid { get; set; }
    }
}
