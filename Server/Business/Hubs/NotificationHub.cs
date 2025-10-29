using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Business.Hubs;

public interface INotificationClient
{
    Task ReceiveNotification(string message);
}

[Authorize]
public class NotificationHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        Console.WriteLine($"User {userId} connected to NotificationHub");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;
        Console.WriteLine($"User {userId} disconnected from NotificationHub");
        await base.OnDisconnectedAsync(exception);
    }
}
