using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Client;

public class ChatClientReceiver : IChatReceiver
{
    public void OnReceiveMessage(string message)
    {
        if (message.StartsWith("[SERVER]:"))
            Console.ForegroundColor = ConsoleColor.Yellow;
        else
            Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine(message);
        Console.ResetColor();
    }
}