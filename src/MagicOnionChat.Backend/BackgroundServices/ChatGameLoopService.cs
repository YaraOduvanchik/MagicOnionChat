using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Backend.BackgroundServices;

public class ChatGameLoopService(IChatCommandService commandService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

        do
        {
            commandService.ProcessPendingCommands();
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}