namespace WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

public record GetUserWebAppsQuery : IRequest<List<WebAppDto>>
{
    public Guid UserGuid { get; set; }
}