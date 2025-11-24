using MagicOnion.Server.Hubs;
using MagicOnionChat.Backend.Infrastructure.Abstractions;

namespace MagicOnionChat.Backend.Infrastructure.Chat;

public class ChatHub(ChatContextRepository chatRepo) : StreamingHubBase<IChatHub, IChatReceiver>, IChatHub
{
    private ChatContext _chat = default!;
    private string _userName = string.Empty;

    protected override ValueTask OnConnected()
    {
        _chat = chatRepo.Chat;
        _chat.Group.Add(Context.ContextId, Client);
        return ValueTask.CompletedTask;
    }

    protected override ValueTask OnDisconnected()
    {
        _chat.Group.Remove(Context.ContextId);
        return ValueTask.CompletedTask;
    }

    public ValueTask JoinAsync(string userName)
    {
        _userName = userName;
        _chat.CommandQueue.Enqueue(new ChatMessageCommand("[SERVER]", $"{userName} присоединился к чату"));
        return ValueTask.CompletedTask;
    }

    public ValueTask SendAsync(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return ValueTask.CompletedTask;
            
        _chat.CommandQueue.Enqueue(new ChatMessageCommand(_userName, message));
        return ValueTask.CompletedTask;
    }
}

