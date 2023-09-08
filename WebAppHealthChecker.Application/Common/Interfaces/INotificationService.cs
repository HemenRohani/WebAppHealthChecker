using WebAppHealthChecker.Domain.Entities;

namespace WebAppHealthChecker.Application.Common.Interfaces;

public interface INotificationService
{
    void SendAsync(string reciver, string text, CancellationToken stoppingToken);

}
