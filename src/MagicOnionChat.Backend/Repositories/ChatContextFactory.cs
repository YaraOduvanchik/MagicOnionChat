using Cysharp.Runtime.Multicast;
using MagicOnionChat.Backend.Abstractions;
using MagicOnionChat.Backend.Core;

namespace MagicOnionChat.Backend.Repositories;

public class ChatContextFactory(IMulticastGroupProvider groupProvider) : IChatContextFactory
{
    public ChatContext CreateContext() => new(groupProvider);
}
