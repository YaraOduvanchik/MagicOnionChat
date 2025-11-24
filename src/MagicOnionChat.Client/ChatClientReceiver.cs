using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Client;

public class ChatClientReceiver(ConsoleUI ui) : IChatReceiver
{
    public async void OnReceiveMessage(string message)
    {
        await ui.PrintMessageAsync(message);
    }
}
