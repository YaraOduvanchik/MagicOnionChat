namespace MagicOnionChat.Backend.Abstractions;

public interface IChatReceiver
{
    void OnReceiveMessage(string message);
}