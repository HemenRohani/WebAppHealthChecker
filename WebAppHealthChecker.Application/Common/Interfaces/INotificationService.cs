namespace WebAppHealthChecker.Application.Common.Interfaces;

public interface INotificationService
{
    Task<ActionResult> SendAsync(string reciver, string text);

}
