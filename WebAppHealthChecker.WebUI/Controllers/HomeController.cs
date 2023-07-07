using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppHealthChecker.Application.Common;
using WebAppHealthChecker.Application.WebApps.Commands.CreateWebApp;
using WebAppHealthChecker.Application.WebApps.Commands.DeleteWebApp;
using WebAppHealthChecker.Application.WebApps.Commands.UpdateWebApp;
using WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;
using WebAppHealthChecker.Application.WebApps.Queries.GetWebApp;

namespace WebAppHealthChecker.WebUI.Controllers
{

    public class HomeController : Controller
    {
        private readonly ISender _mediator;
        private readonly ILogger<HomeController> _logger;
        private readonly UserData _userData;

        public HomeController(ISender mediator, ILogger<HomeController> logger, UserData userData)
        {
            _mediator = mediator;
            _logger = logger;
            _userData = userData;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<List<WebAppDto>> WebAppsList()
        {
            var request = new GetUserWebAppsQuery
            {
                UserGuid = _userData.Guid
            };
            return await _mediator.Send(request);
        }

        [Authorize]
        public ActionResult CreateWebApp()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateWebApp(CreateWebAppCommand createWebAppCommand)
        {
            await _mediator.Send(createWebAppCommand);
            return Ok("Ok");
        }

        [Authorize]
        public async Task<ActionResult> UpdateWebApp(Guid guid)
        {
            var webApp = await _mediator.Send(new GetWebAppQuery { Guid = guid });
            var model = new UpdateWebAppCommand
            {
                Guid = webApp.Guid,
                CheckInterval = webApp.CheckInterval,
                Name = webApp.Name,
                URL = webApp.URL
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateWebApp(UpdateWebAppCommand updateWebAppCommand)
        {
            await _mediator.Send(updateWebAppCommand);
            return Ok("Ok");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> DeleteWebApp(DeleteWebAppCommand deleteWebAppCommand)
        {
            await _mediator.Send(deleteWebAppCommand);
            return Ok("Ok");
        }
    }
}