using MagicOnion.Server.Hubs;
using MagicOnionChat.Backend.Abstractions;

namespace MagicOnionChat.Backend.Core;

public class ChatHub(IChatContextRepository chatRepository) : StreamingHubBase<IChatHub, IChatReceiver>, IChatHub
{
    private string _userName = string.Empty;

    protected override ValueTask OnConnected()
    {
        chatRepository.RegisterClient(Context.ContextId, Client);
        return default;
    }

    protected override ValueTask OnDisconnected()
    {
        chatRepository.UnregisterClient(Context.ContextId);
        return default;
    }

    public ValueTask JoinAsync(string userName)
    {
        _userName = ValidateName(userName);
        chatRepository.EnqueueSystemMessage($"{_userName} присоединился к чату");
        return default;
    }

    public ValueTask SendAsync(string message)
    {
        chatRepository.EnqueueMessage(_userName, message);
        return default;
    }

    private static string ValidateName(string userName) =>
        string.IsNullOrWhiteSpace(userName) ? "Anonymous" : userName;
}
