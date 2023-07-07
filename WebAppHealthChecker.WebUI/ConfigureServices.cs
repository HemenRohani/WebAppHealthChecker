using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Principal;
using WatchDog;
using WebAppHealthChecker.Application.Common;
using WebAppHealthChecker.Application.Common.Interfaces.AAA;
using WebAppHealthChecker.Domain.Entities;
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

        services.AddWatchDogServices(opt => 
        {
            opt.SetExternalDbConnString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=WebAppHealthCheckerDb;MultipleActiveResultSets=true;Encrypt=false";
            opt.DbDriverOption = WatchDog.src.Enums.WatchDogDbDriverEnum.MSSQL;
        });

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