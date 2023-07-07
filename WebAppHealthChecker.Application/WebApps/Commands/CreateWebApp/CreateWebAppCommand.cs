using System.ComponentModel.DataAnnotations;

namespace WebAppHealthChecker.Application.WebApps.Commands.CreateWebApp;

public record CreateWebAppCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string URL { get; set; }
    public int CheckInterval { get; set; }
}