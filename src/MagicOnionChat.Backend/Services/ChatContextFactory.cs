using Cysharp.Runtime.Multicast;
using MagicOnionChat.Backend.Core;

namespace MagicOnionChat.Backend.Services;

public class ChatContextFactory(IMulticastGroupProvider groupProvider)
{
    public ChatContext CreateContext() => new(groupProvider);
}
