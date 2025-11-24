using MagicOnionChat.Backend.Infrastructure.Chat;

namespace MagicOnionChat.Backend.Infrastructure.Abstractions;

public interface ICommand
{
    void Execute(ChatContext ctx);
}