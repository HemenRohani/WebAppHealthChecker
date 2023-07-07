using System.ComponentModel.DataAnnotations;

namespace WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

public class WebAppWithUserInfoDto
{

    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserEmail { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }
    public int CheckInterval { get; set; }
}