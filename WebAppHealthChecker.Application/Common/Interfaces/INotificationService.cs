namespace WebAppHealthChecker.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendAsync(string reciver, string text);

}
