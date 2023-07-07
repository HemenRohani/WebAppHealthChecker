using ElmahCore;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WebAppHealthChecker.Application.Common;
using WebAppHealthChecker.Application.Common.Interfaces;
using WebAppHealthChecker.Application.Common.Interfaces.AAA;
using WebAppHealthChecker.Infrastructure.AAA;
using WebAppHealthChecker.Infrastructure.NotificationService;
using WebAppHealthChecker.WebUI.Helper;

namespace WebAppHealthChecker.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection RegisterPresentationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IDeviceDetectionService, DeviceDetectionService>();
        services.AddScoped<ICookieValidatorService, CookieValidatorService>();

        services.AddElmah<XmlFileErrorLog>(options =>
        {
            options.LogPath = "~/log";
            options.OnPermissionCheck = context => context.User.Identity.IsAuthenticated;
        });

        //services.AddHostedService<WebAppHealthCheckerTask>();

        services.AddTransient<UserData>(provider => 
        {
            var user = provider.GetService<IHttpContextAccessor>()?.HttpContext?.User;

            var userIdString = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userIdString, out var userId);
            var userGuidString = user?.FindFirst(ClaimTypes.UserData)?.Value;
            Guid.TryParse(userGuidString, out var userGuid);

            return new UserData
            {
                Id = userId,
                Guid = userGuid
            };
        });


        services.AddScoped<INotificationService, EmailService>();
        //Add more INotificationService here

        services
        .AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.SlidingExpiration = false;
                options.LoginPath = "/Authentication/login";
                options.LogoutPath = "/Authentication/logout";
                //options.AccessDeniedPath = new PathString("/Home/Forbidden/");
                options.Cookie.Name = ".web.app.health.checker.cookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = context =>
                {
                var cookieValidatorService = context.HttpContext.RequestServices
                    .GetRequiredService<ICookieValidatorService>();
                return cookieValidatorService.ValidateAsync(context);
            }
                };
            });


        return services;
    }
}