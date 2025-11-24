using MagicOnionChat.Backend.Core;
using MagicOnionChat.Backend.Repositories;

namespace MagicOnionChat.Backend.BackgroundServices;

public class ChatNotificationService(ChatContextRepository repo) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var chatContext = repo.Chat;
        int counter = 1;
        
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(15));

        do
        {
            var message = $"Системное оповещение #{counter}";
            chatContext.CommandQueue.Enqueue(new ChatMessageCommand("[SERVER]", message));
            counter++;

        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}
