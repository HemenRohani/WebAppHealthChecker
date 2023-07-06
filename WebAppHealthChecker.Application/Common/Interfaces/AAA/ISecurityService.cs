namespace WebAppHealthChecker.Application.Common.Interfaces.AAA;

public interface ISecurityService
{
    string GetSha256Hash(string input);
}