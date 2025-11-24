using MagicOnionChat.Backend.Infrastructure.Abstractions;

namespace MagicOnionChat.Backend.Infrastructure.Chat;

public class ChatMessageCommand(string user, string message) : ICommand
{
    public void Execute(ChatContext ctx)
    {
        ctx.Group.All.OnReceiveMessage($"{user}: {message}");
    }
}