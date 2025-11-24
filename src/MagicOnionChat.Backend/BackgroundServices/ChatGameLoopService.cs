using MagicOnionChat.Backend.Repositories;

namespace MagicOnionChat.Backend.BackgroundServices;

public class ChatGameLoopService(ChatContextRepository chatRepo) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var chatContext = chatRepo.Chat;

        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

        do
        {
            while (chatContext.CommandQueue.TryDequeue(out var command))
            {
                command.Execute(chatContext);
            }
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}