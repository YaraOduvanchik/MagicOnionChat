using System.Collections.Concurrent;
using Cysharp.Runtime.Multicast;
using MagicOnionChat.Backend.Infrastructure.Abstractions;

namespace MagicOnionChat.Backend.Infrastructure.Chat;

public class ChatContext
{
    public ConcurrentQueue<ICommand> CommandQueue { get; } = new();

    public IMulticastSyncGroup<Guid, IChatReceiver> Group { get; }

    public ChatContext(IMulticastGroupProvider groupProvider)
    {
        Group = groupProvider.GetOrAddSynchronousGroup<Guid, IChatReceiver>("MainChat");
    }
}
