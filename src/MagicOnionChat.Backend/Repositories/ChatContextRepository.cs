using Cysharp.Runtime.Multicast;
using MagicOnionChat.Backend.Abstractions;
using MagicOnionChat.Backend.Core;

namespace MagicOnionChat.Backend.Repositories;

public class ChatContextRepository : IChatContextRepository
{
    private readonly ChatContext _chatContext;

    public ChatContextRepository(IMulticastGroupProvider groupProvider)
    {
        _chatContext = new ChatContext(groupProvider);
    }

    public void RegisterClient(Guid connectionId, IChatReceiver receiver)
    {
        _chatContext.Group.Add(connectionId, receiver);
    }

    public void UnregisterClient(Guid connectionId)
    {
        _chatContext.Group.Remove(connectionId);
    }

    public void EnqueueSystemMessage(string message) => EnqueueMessage("[SERVER]", message);
    
    public void EnqueueMessage(string author, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return;

        _chatContext.CommandQueue.Enqueue(new ChatMessageCommand(author, message));
    }

    public void ProcessPendingCommands()
    {
        while (_chatContext.CommandQueue.TryDequeue(out var command))
        {
            command.Execute(_chatContext);
        }
    }
}
