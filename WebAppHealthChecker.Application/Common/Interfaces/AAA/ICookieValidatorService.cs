using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebAppHealthChecker.Application.Common.Interfaces.AAA
{
    public interface ICookieValidatorService
    {
        Task ValidateAsync(CookieValidatePrincipalContext context);
    }
}
