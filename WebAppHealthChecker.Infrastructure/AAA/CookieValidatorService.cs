using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WebAppHealthChecker.Application.Common.Interfaces.AAA;
using WebAppHealthChecker.Application.Users.Queries.GetUser;

namespace WebAppHealthChecker.Infrastructure.AAA;

public class CookieValidatorService : ICookieValidatorService
{
    private ISender _mediator;
    private readonly IDeviceDetectionService _deviceDetectionService;

    public CookieValidatorService(ISender mediator, IDeviceDetectionService deviceDetectionService)
    {
        _mediator = mediator;
        _deviceDetectionService =
            deviceDetectionService ?? throw new ArgumentNullException(nameof(deviceDetectionService));
    }

    public async Task ValidateAsync(CookieValidatePrincipalContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
        if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
        {
            // this is not our issued cookie
            await handleUnauthorizedRequest(context);
            return;
        }

        if (!_deviceDetectionService.HasUserTokenValidDeviceDetails(claimsIdentity))
        {
            // Detected usage of an old token from a new device! Please login again!
            await handleUnauthorizedRequest(context);
            return;
        }

        var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
        if (serialNumberClaim == null)
        {
            // this is not our issued cookie
            await handleUnauthorizedRequest(context);
            return;
        }

        var userGuidString = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;
        if (!Guid.TryParse(userGuidString, out var userGuid))
        {
            // this is not our issued cookie
            await handleUnauthorizedRequest(context);
            return;
        }

        var user = await _mediator.Send(new GetUserQuery { Guid = userGuid });
        if (user == null)
        {
            // user has changed his/her password/roles/stat/IsActive
            await handleUnauthorizedRequest(context);
        }

    }

    private static Task handleUnauthorizedRequest(CookieValidatePrincipalContext context)
    {
        context.RejectPrincipal();
        return context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}