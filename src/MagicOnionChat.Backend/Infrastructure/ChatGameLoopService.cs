using MagicOnionChat.Backend.Infrastructure.Chat;

namespace MagicOnionChat.Backend.Infrastructure;

public class ChatGameLoopService(ChatContextRepository chatRepo) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var chatContext = chatRepo.Chat;

        var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

        do
        {
            while (chatContext.CommandQueue.TryDequeue(out var command))
            {
                command.Execute(chatContext);
            }
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}