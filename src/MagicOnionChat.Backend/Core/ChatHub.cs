using MagicOnion.Server.Hubs;
using MagicOnionChat.Backend.Abstractions;
using MagicOnionChat.Contracts;

namespace MagicOnionChat.Backend.Core;

public class ChatHub(IChatCommandService commandService) : StreamingHubBase<IChatHub, IChatReceiver>, IChatHub
{
    private string _userName = string.Empty;

    protected override ValueTask OnConnected()
    {
        commandService.RegisterClient(Context.ContextId, Client);
        return default;
    }

    protected override ValueTask OnDisconnected()
    {
        commandService.UnregisterClient(Context.ContextId);
        return default;
    }

    public ValueTask JoinAsync(string userName)
    {
        _userName = string.IsNullOrWhiteSpace(userName) ? "Anonymous" : userName;
        commandService.EnqueueSystemMessage($"{_userName} присоединился к чату");
        return default;
    }

    public ValueTask SendAsync(string message)
    {
        commandService.EnqueueMessage(_userName, message);
        return default;
    }
}
