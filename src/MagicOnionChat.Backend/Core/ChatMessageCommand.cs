using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Backend.Core;

public class ChatMessageCommand(string user, string message) : ICommand
{
    public void Execute(ChatContext context)
    {
        context.Group.All.OnReceiveMessage($"{user}: {message}");
    }
}
