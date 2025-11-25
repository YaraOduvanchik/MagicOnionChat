using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Backend.BackgroundServices;

public class ChatNotificationService(IChatCommandService commandService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int counter = 1;

        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(15));

        do
        {
            var message = $"Системное оповещение #{counter}";
            commandService.EnqueueSystemMessage(message);
            counter++;
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}