using MagicOnionChat.Backend.Core;

namespace MagicOnionChat.Backend.Abstractions;

public interface ICommand
{
    void Execute(ChatContext context);
}