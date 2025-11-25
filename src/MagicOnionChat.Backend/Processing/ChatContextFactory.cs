using Cysharp.Runtime.Multicast;
using MagicOnionChat.Backend.Core;

namespace MagicOnionChat.Backend.Processing;

public class ChatContextFactory(IMulticastGroupProvider groupProvider)
{
    public ChatContext CreateContext() => new(groupProvider);
}
