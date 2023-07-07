using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppHealthChecker.Application.Authentication.Commands.UserRegister;
using WebAppHealthChecker.Application.Authentication.Queries.Login;
using WebAppHealthChecker.Application.Common.Interfaces.AAA;

namespace WebAppHealthChecker.WebUI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ISender _mediator;
        private readonly ILogger<HomeController> _logger;
        private readonly IDeviceDetectionService _deviceDetectionService;

        public AuthenticationController(ISender mediator, ILogger<HomeController> logger, IDeviceDetectionService deviceDetectionService)
        {
            _mediator = mediator;
            _logger = logger;
            _deviceDetectionService = deviceDetectionService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginQuery loginQuery)
        {
            var user = await _mediator.Send(loginQuery);
            if (user == null)
            {
                return Unauthorized();
            }

            var cookieClaims = createCookieClaims(user);

            await HttpContext.SignInAsync(
                               CookieAuthenticationDefaults.AuthenticationScheme,
                               cookieClaims,
                               new AuthenticationProperties
                               {
                                   IsPersistent = true, // "Remember Me"
                                   IssuedUtc = DateTimeOffset.UtcNow,
                                   ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
                               });

            return Ok("Ok");

        }

        private ClaimsPrincipal createCookieClaims(UserDto user)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}({user.Email})"));
            identity.AddClaim(new Claim("DisplayName", $"{user.FirstName} {user.LastName}"));

            // to invalidate the cookie
            identity.AddClaim(new Claim(ClaimTypes.SerialNumber, user.Guid.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.System, _deviceDetectionService.GetCurrentRequestDeviceDetailsHash(),
                                        ClaimValueTypes.String));

            // custom data
            identity.AddClaim(new Claim(ClaimTypes.UserData, user.Guid.ToString()));

            return new ClaimsPrincipal(identity);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> Register(UserRegisterCommand userRegisterCommand)
        {
            await _mediator.Send(userRegisterCommand);
            return Ok("Ok");
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home");
        }

    }
}
