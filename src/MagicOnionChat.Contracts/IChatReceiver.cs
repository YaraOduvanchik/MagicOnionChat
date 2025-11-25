namespace MagicOnionChat.Contracts;

public interface IChatReceiver
{
    void OnReceiveMessage(string message);
}