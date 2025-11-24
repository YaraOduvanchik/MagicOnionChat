using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Client;

public class ChatClient
{
    private readonly string _serverUrl;
    private readonly ConsoleUI _ui;
    private GrpcChannel? _channel;
    private IChatHub? _hub;
    private string _userName = string.Empty;

    public ChatClient(string serverUrl, ConsoleUI ui)
    {
        _serverUrl = serverUrl;
        _ui = ui;
    }

    public async ValueTask RunAsync()
    {
        _userName = await _ui.ReadUserNameAsync();

        try
        {
            _channel = CreateGrpcChannel();
            _hub = await ConnectToHubAsync();
            
            UpdateConsoleTitle();
            await _ui.PrintWelcomeAsync(_userName);
            
            await _hub.JoinAsync(_userName);
            await MessageLoopAsync();
        }
        catch (Exception ex)
        {
           await _ui.PrintErrorAsync(ex.Message);
        }
        finally
        {
            await CleanupAsync();
        }
    }

    private GrpcChannel CreateGrpcChannel()
    {
        var httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        return GrpcChannel.ForAddress(_serverUrl, new GrpcChannelOptions
        {
            HttpHandler = httpHandler,
            MaxReceiveMessageSize = null
        });
    }

    private async ValueTask<IChatHub> ConnectToHubAsync()
    {
        return await StreamingHubClient.ConnectAsync<IChatHub, IChatReceiver>(
            _channel!,
            new ChatClientReceiver(_ui));
    }

    private void UpdateConsoleTitle()
    {
        Console.Title = $"MagicOnion Chat - {_userName}";
    }

    private async ValueTask MessageLoopAsync()
    {
        while (true)
        {
            var message = await _ui.ReadMessageAsync();

            if (IsExitCommand(message))
            {
                await _ui.PrintExitAsync();
                break;
            }

            if (string.IsNullOrWhiteSpace(message))
                continue;

            await SendMessageAsync(message.Trim());
        }
    }

    private static bool IsExitCommand(string? input) =>
        string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase);

    private async ValueTask SendMessageAsync(string message)
    {
        try
        {
            await _hub!.SendAsync(message);
        }
        catch (Exception ex)
        {
            await _ui.PrintErrorAsync($"Ошибка отправки: {ex.Message}");
        }
    }

    private async ValueTask CleanupAsync()
    {
        if (_hub != null)
            await _hub.DisposeAsync();

        _channel?.Dispose();
    }
}