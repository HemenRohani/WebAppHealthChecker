using Microsoft.AspNetCore.Authentication.Cookies;
using WebAppHealthChecker.Application.Common.Interfaces.AAA;
using WebAppHealthChecker.Infrastructure.AAA;

namespace WebAppHealthChecker.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection RegisterPresentationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IDeviceDetectionService, DeviceDetectionService>();
        services.AddScoped<ICookieValidatorService, CookieValidatorService>();

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