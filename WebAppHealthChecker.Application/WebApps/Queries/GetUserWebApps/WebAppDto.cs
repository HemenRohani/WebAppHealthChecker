using System.ComponentModel.DataAnnotations;

namespace WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

public class WebAppDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }
    public int CheckInterval { get; set; }
    public DateTime? LastCheck { get; set; }
    public int LastStatusCode { get; set; }
}