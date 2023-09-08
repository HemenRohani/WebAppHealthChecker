using MediatR;
using System;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Threading.Tasks;
using WebAppHealthChecker.Application.Common.Interfaces;
using WebAppHealthChecker.Application.WebApps.Commands.UpdateWebAppStatus;
using WebAppHealthChecker.Application.WebApps.Queries.GetAllWebApps;
using WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

namespace WebAppHealthChecker.WebUI.Helper
{
    public class WebAppHealthCheckerTask : ScheduledScopedBackgroundService
    {
        public static bool NeedToUpdateSites = true;
        private readonly ILogger<WebAppHealthCheckerTask> _logger;
        private static List<WebAppWithUserInfoDto> webAppsList = new List<WebAppWithUserInfoDto>(); 

        public WebAppHealthCheckerTask(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<WebAppHealthCheckerTask> logger) : base(serviceScopeFactory)
        {
            _logger = logger;
        }

        protected override string Schedule => "*/10 * * * * *"; //Runs every 1 seconds

        public override async Task ScheduledExecuteInScope(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        {
            if (NeedToUpdateSites)
            {
                var mediator = serviceProvider.GetService<ISender>();
                webAppsList = await mediator.Send(new GetAllWebAppsQuery { });
                NeedToUpdateSites = false;
            }
            var tasks = new List<Task>();

            foreach (var webApp in webAppsList)
            {
                tasks.Add(Task.Run(async () => {
                    
                    try
                    {
                        var checkingResponse = await (new HttpClient()).GetAsync(webApp.URL);
                        var now = DateTime.Now;
                        var mediator = serviceProvider.GetService<ISender>();
                        mediator.Send(new UpdateWebAppStatusCommand { LastCheck = now, LastStatusCode = checkingResponse.StatusCode.GetHashCode() });
                        if ((checkingResponse.StatusCode.GetHashCode() / 100) == 2)
                        {
                            var services = serviceProvider.GetServices<INotificationService>();

                            foreach (var service in services)
                            {
                                await service.SendAsync(webApp.UserEmail, $"Unable to reach {webApp.URL} at {now}", stoppingToken);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }));
            }
            await Task.WhenAll(tasks);
            //_logger.LogInformation("WebAppHealthCheckerTask executing - {0}", DateTime.Now);
            return;
        }
    }
}
