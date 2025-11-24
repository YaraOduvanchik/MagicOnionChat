using MagicOnionChat.Backend.Core;

namespace MagicOnionChat.Backend.Abstractions;

public interface IChatContextRepository
{
    void RegisterClient(Guid connectionId, IChatReceiver receiver);
    void UnregisterClient(Guid connectionId);
    void EnqueueMessage(string author, string message);
    void EnqueueSystemMessage(string message);
    void ProcessPendingCommands();
}
