using MagicOnionChat.Backend.Core;

namespace MagicOnionChat.Backend.Abstractions;

public interface IChatContextFactory
{
    ChatContext CreateContext();
}
