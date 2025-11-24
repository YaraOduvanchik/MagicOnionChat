using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Backend.BackgroundServices;

public class ChatGameLoopService(IChatContextRepository chatRepository) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

        do
        {
            chatRepository.ProcessPendingCommands();
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}
