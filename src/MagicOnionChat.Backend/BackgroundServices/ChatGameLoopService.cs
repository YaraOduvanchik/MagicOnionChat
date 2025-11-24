using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Backend.BackgroundServices;

public class ChatGameLoopService(IChatCommandProcessor commandProcessor) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

        do
        {
            commandProcessor.ProcessPendingCommands();
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}