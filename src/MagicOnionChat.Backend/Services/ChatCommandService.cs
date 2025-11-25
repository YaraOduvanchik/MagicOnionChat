using MagicOnionChat.Backend.Abstractions;
using MagicOnionChat.Backend.Core;
using MagicOnionChat.Contracts;

namespace MagicOnionChat.Backend.Services;

public class ChatCommandService : IChatCommandService
{
    private readonly ChatContext _chatContext;

    public ChatCommandService(ChatContextFactory contextFactory)
    {
        _chatContext = contextFactory.CreateContext();
    }

    public void RegisterClient(Guid connectionId, IChatReceiver receiver)
    {
        _chatContext.Group.Add(connectionId, receiver);
    }

    public void UnregisterClient(Guid connectionId)
    {
        _chatContext.Group.Remove(connectionId);
    }

    public void EnqueueMessage(string author, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return;

        _chatContext.CommandQueue.Enqueue(new ChatMessageCommand(author, message));
    }

    public void EnqueueSystemMessage(string message) =>
        EnqueueMessage("[SERVER]", message);

    public void ProcessPendingCommands()
    {
        while (_chatContext.CommandQueue.TryDequeue(out var command))
        {
            command.Execute(_chatContext);
        }
    }
}
