using System.Security.Cryptography;
using System.Text;
using WebAppHealthChecker.Application.Common.Interfaces.AAA;

namespace WebAppHealthChecker.Infrastructure.AAA;

public class SecurityService : ISecurityService
{
    public string GetSha256Hash(string input)
    {
        var byteValue = Encoding.UTF8.GetBytes(input);
        var byteHash = SHA256.HashData(byteValue);
        return Convert.ToBase64String(byteHash);
    }
}