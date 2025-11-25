using MagicOnionChat.Contracts;

namespace MagicOnionChat.Backend.Abstractions;

public interface IChatCommandService
{
    void RegisterClient(Guid connectionId, IChatReceiver receiver);
    void UnregisterClient(Guid connectionId);
    void EnqueueMessage(string author, string message);
    void EnqueueSystemMessage(string message);
    void ProcessPendingCommands();
}
