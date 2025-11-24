using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Client;

public class ChatClient
{
    private readonly string _serverUrl;
    private GrpcChannel? _channel;
    private IChatHub? _hub;
    private string _userName = string.Empty;

    public ChatClient(string serverUrl)
    {
        _serverUrl = serverUrl;
    }

    public async ValueTask RunAsync()
    {
        Console.Write("Введите ваше имя: ");
        _userName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(_userName))
            _userName = "Anonymous";

        var httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        _channel = GrpcChannel.ForAddress(_serverUrl, new GrpcChannelOptions
        {
            HttpHandler = httpHandler
        });

        try
        {
            _hub = await StreamingHubClient.ConnectAsync<IChatHub, IChatReceiver>(
                _channel,
                new ChatClientReceiver());

            Console.WriteLine($"\nПодключены как: {_userName}\n");

            await _hub.JoinAsync(_userName);

            await MessageLoopAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка подключения: {ex.Message}");
        }
        finally
        {
            if (_hub != null)
                await _hub.DisposeAsync();
            
            if (_channel != null)
                _channel.Dispose();
        }
    }

    private async ValueTask MessageLoopAsync()
    {
        Console.WriteLine("Введите сообщение (или 'exit' для выхода):\n");

        while (true)
        {
            Console.Write("> ");
            var message = Console.ReadLine();

            if (string.Equals(message, "exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Выход из чата...");
                break;
            }

            if (string.IsNullOrWhiteSpace(message))
                continue;

            try
            {
                await _hub!.SendAsync(message.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки: {ex.Message}");
            }
        }
    }
}