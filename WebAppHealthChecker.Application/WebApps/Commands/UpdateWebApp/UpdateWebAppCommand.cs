using System.ComponentModel.DataAnnotations;

namespace WebAppHealthChecker.Application.WebApps.Commands.UpdateWebApp;

public record UpdateWebAppCommand : IRequest<Guid>
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }
    public int CheckInterval { get; set; }
}