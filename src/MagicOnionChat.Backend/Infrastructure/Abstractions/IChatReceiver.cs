namespace MagicOnionChat.Backend.Infrastructure.Abstractions;

public interface IChatReceiver
{
    void OnReceiveMessage(string message);
}