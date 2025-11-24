using Cysharp.Runtime.Multicast;
using MagicOnionChat.Backend.Core;

namespace MagicOnionChat.Backend.Repositories;

public class ChatContextRepository(IMulticastGroupProvider groupProvider)
{
    public ChatContext Chat { get; } = new(groupProvider);
}