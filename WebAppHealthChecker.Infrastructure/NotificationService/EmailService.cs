using WebAppHealthChecker.Application.Common;
using WebAppHealthChecker.Application.Common.Interfaces;

namespace WebAppHealthChecker.Infrastructure.NotificationService;

public class EmailService : INotificationService
{
    public Task SendAsync(string reciver, string text)
    {
        throw new NotImplementedException();
    }
}
